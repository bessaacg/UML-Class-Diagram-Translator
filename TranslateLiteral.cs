using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UMLClassDiagramTranslator
{
    internal class TranslateLiteral
    {
        static XmlDocument xmlDoc = new XmlDocument();
        static XmlNodeList? classNodes;
        static List<ClassRelationship> classRelationships  = new List<ClassRelationship>();


        public static string Translate(string path)
        {
            StringBuilder sb = new StringBuilder();
            string filePath = path;
            

            try
            {
                xmlDoc.Load(filePath);
            }
            catch
            {
                MessageBox.Show("Please load file before translating!");
            }

            classNodes = xmlDoc.SelectNodes("//Entity[@type='Class']");
            classRelationships = ParseXMLFile(filePath);


            //Number of boxes
            int classCount = CountClasses(filePath);
            //Console.WriteLine("There are {0} boxes from the diagram", classCount);
            if (classCount == 1)
                sb.Append(String.Format("There is {0} box from the diagram", classCount));
            else
                sb.Append(String.Format("There are {0} boxes from the diagram", classCount));


            //shape types for the boxes
            var classShapes = GetClassShapes(filePath);
            foreach (var classShape in classShapes)
            {
                //Console.WriteLine("The box titled {0} is a {1}", classShape.ClassName, classShape.ShapeType);
                if(classCount == 1)
                    sb.Append(String.Format("\nThe box is titled {0} and it is a {1}", classShape.ClassName, classShape.ShapeType));
                else
                    sb.Append(String.Format("\nThe box titled {0} is a {1}", classShape.ClassName, classShape.ShapeType));

            }

            //The number of compartments of each box and
            //the number of horizontal lines separating those compartments
            List<BoxInfo> boxes = new List<BoxInfo>();

            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(filePath);
            //XmlNodeList? entityNodes = xmlDoc.SelectNodes("//Entity[@type='Class']");
            if (classNodes is not null)
            {
                foreach (XmlNode? entityNode in classNodes)
                {
                    if (entityNode is not null)
                    {
                        BoxInfo boxInfo = new BoxInfo();

                        XmlNode? nameNode = entityNode.SelectSingleNode("Name");
                        if (nameNode is not null)
                        {
                            boxInfo.Name = nameNode.InnerText;
                        }
                        else
                        {
                            // Handle the case when 'Name' node is null or not found.
                            boxInfo.Name = "";
                        }

                        boxInfo.Compartments = CountCompartments(entityNode);
                        boxes.Add(boxInfo);
                    }
                }
            }


            Console.WriteLine();

            foreach (BoxInfo boxInfo in boxes)
            {
                string bInfo = String.Format("\n{0} box has {1} compartments with text seperated by {2} horizotal lines",
                    boxInfo.Name, boxInfo.Compartments, boxInfo.Compartments - 1);
                Console.WriteLine(bInfo);
                sb.Append(bInfo);
            }


            //computation of boxes' compartment details
            //XmlNodeList entityNodes1 = xmlDoc.SelectNodes("//Entity[@type='Class']");

            List<BoxInfoCompartments> bxes = new List<BoxInfoCompartments>();

            if (classNodes is not null)
            {
                foreach (XmlNode entityNode in classNodes)
                {
                    if (entityNode is not null)
                    {
                        BoxInfoCompartments boxInfoComp = new BoxInfoCompartments();

                        XmlNode? nameNode = entityNode.SelectSingleNode("Name");
                        if (nameNode is not null)
                        {
                            boxInfoComp.Name = nameNode.InnerText;
                        }
                        else
                        {
                            // Handle the case when 'Name' node is null or not found.
                            boxInfoComp.Name = "";
                        }

                        boxInfoComp.CompartmentsInfo = ExtractCompartments(entityNode);
                        bxes.Add(boxInfoComp);
                    }
                }
            }
            
            foreach (BoxInfoCompartments boxInfo in bxes)
            {
                string bInfoHeadings = String.Format("\n{0} box has the following text in its {1} compartments",
                    boxInfo.Name, boxInfo.CompartmentsInfo.Count);
                Console.WriteLine(bInfoHeadings);
                sb.Append(bInfoHeadings);

                for (int i = 0; i < boxInfo.CompartmentsInfo.Count; i++)
                {
                    string compartItems = String.Join("\n", boxInfo.CompartmentsInfo[i]);
                    string compartInfo = String.Format("\nCompartment {0} of {1} box contains the " +
                        "following text\n{2}",
                        i + 1, boxInfo.Name, compartItems);

                    //Console.WriteLine("{0}", compartInfo);
                    sb.Append(compartInfo);

                    Console.WriteLine();
                }
            }

            string relationShips = ComputeRelationships(filePath);

            Console.WriteLine(relationShips);

            return sb.ToString() + relationShips;
        }

        static string ComputeRelationships(string path)
        {
            StringBuilder relationShips = new StringBuilder();

            List<ClassRelationship> classRelationships = ParseXMLFile(path);

            // Print the class relationships
            foreach (ClassRelationship relationship in classRelationships)
            {
                string head = String.Empty;
                string tail = String.Empty;
                string cRelationship = String.Empty;


                if (relationship.RelationshipType == "Aggregation")
                    head = "unshaded diamond";
                else if (relationship.RelationshipType == "Association")
                {
                    if (relationship.AssociationType == "Composition")
                        head = "shaded diamond";
                    else if (relationship.AssociationType == "Aggregation")
                        head = "an unshaded diamond";
                    else
                        head = "straight line";

                }


                else if (relationship.RelationshipType == "Generalization")
                {
                    string temp = relationship.FirstClass;
                    relationship.FirstClass = relationship.SecondClass;
                    relationship.SecondClass = temp;
                    head = "unshaded triangle";
                    tail = relationship.RelationshipSide;
                }
                else if (relationship.RelationshipType == "Dependency")
                {
                    string temp = relationship.FirstClass;
                    relationship.FirstClass = relationship.SecondClass;
                    relationship.SecondClass = temp;
                    head = "arrow";
                    tail = "dashed line";
                }

                if ((relationship.RelationshipSide == "Vertical" ||
                    relationship.RelationshipSide == "Horizontal"))
                {
                    if (relationship.RelationshipDirection == "Unidirectional")
                        tail = "with an arrow";
                    else
                        tail = "line";
                }


                if ((head == "straight line" && (tail == "line"
                    || tail == "Horizontal")))
                    cRelationship = String.Format("There is a straight line connecting {0} box " +
                        "and {1} box.", relationship.FirstClass, relationship.SecondClass);

                else
                    cRelationship = String.Format("{0} box is connected by {1} with a {2}" +
                            " joining {3} box", relationship.FirstClass, head, tail, relationship.SecondClass);

                relationShips.Append(String.Format("\n{0}", cRelationship));
            }

            return  relationShips.ToString();
        }

        public static List<ClassRelationship> ParseXMLFile(string filePath)
        {
            List<ClassRelationship> classRelationships = new List<ClassRelationship>();

            XmlNodeList? relationshipNodes = xmlDoc.SelectNodes("//Relationships/Relationship");

            Dictionary<string, string> ? classDictionary = new Dictionary<string, string>();

            if (classNodes != null)
            {
                for (int i = 0; i < classNodes.Count; i++)
                {
                    XmlNode? entityNode = classNodes[i];
                    if (entityNode != null)
                    {
                        string? className = entityNode.SelectSingleNode("Name")?.InnerText;
                        string classIndex = i.ToString();

                        if (!string.IsNullOrEmpty(className))
                        {
                            classDictionary.Add(classIndex, className);
                        }
                    }
                }
            }


            if (relationshipNodes != null)
            {
                foreach (XmlNode relationshipNode in relationshipNodes)
                {
                    string firstClassIndex = relationshipNode.Attributes["first"]?.Value;
                    string secondClassIndex = relationshipNode.Attributes["second"]?.Value;
                    string relationshipType = relationshipNode.Attributes["type"]?.Value;
                    string relationshipSide = relationshipNode.SelectSingleNode("StartOrientation")?.InnerText;
                    string relationshipDirection = relationshipNode.SelectSingleNode("Direction")?.InnerText;
                    string associationType = relationshipNode.SelectSingleNode("AssociationType")?.InnerText;

                    if (firstClassIndex != null && secondClassIndex != null && relationshipType != null)
                    {
                        // Code to handle non-null values
                        string firstClass = classDictionary[firstClassIndex];
                        string secondClass = classDictionary[secondClassIndex];

                        ClassRelationship classRelationship = new ClassRelationship
                        {
                            FirstClass = firstClass,
                            SecondClass = secondClass,
                            RelationshipType = relationshipType,
                            RelationshipSide = relationshipSide,
                            RelationshipDirection = relationshipDirection,
                            AssociationType = associationType
                        };

                        classRelationships.Add(classRelationship);
                    }

                }
            }


            return classRelationships;
        }

        static int CountClasses(string filePath)
        {
            XmlNodeList? classNodes = xmlDoc.SelectNodes("//Entity[@type='Class']");

            int classCount = 0;

            if (classNodes != null)
            {
                classCount = classNodes.Count;
            }
            
            return classCount;
        }

        static ClassShape[] GetClassShapes(string filePath)
        {
            //XmlNodeList classNodes = xmlDoc.SelectNodes("//Entity[@type='Class']");
            ClassShape[] classShapes = new ClassShape[classNodes.Count];

            for (int i = 0; i < classNodes.Count; i++)
            {
                XmlNode? classNode = classNodes[i];
                if(classNode != null)
                {
                    string? className = classNode.SelectSingleNode("Name")?.InnerText;
                    string shapeType = DetermineShapeType(classNode);

                    classShapes[i] = new ClassShape(className, shapeType);
                }
                
            }

            return classShapes;
        }

        static string DetermineShapeType(XmlNode classNode)
        {
            int width = Convert.ToInt32(classNode.SelectSingleNode("Size/@width")?.Value);
            int height = Convert.ToInt32(classNode.SelectSingleNode("Size/@height")?.Value);

            if (width == height)
            {
                return "Square";
            }
            else
            {
                return "Rectangle";
            }
        }

        //number of compartments for each box
        static int CountCompartments(XmlNode classNode)
        {
            string className = classNode.SelectSingleNode("Name").InnerText;

            int numFields = classNode.SelectNodes("Member[@type='Field']").Count;
            int numMethods = classNode.SelectNodes("Member[@type='Method']").Count;

            int numCompartments = 1 + (numFields > 0 ? 1 : 0) + (numMethods > 0 ? 1 : 0);

            return numCompartments;
        }

        static List<List<string>> ExtractCompartments(XmlNode classNode)
        {
            List<List<string>> compartments = new List<List<string>>();

            string className = classNode.SelectSingleNode("Name").InnerText;
            compartments.Add(new List<string> { className });

            XmlNodeList? memberNodes = classNode.SelectNodes("Member");
            List<string> fieldCompartments = new List<string>();
            List<string> methodCompartments = new List<string>();

            if (memberNodes != null)
            {

                foreach (XmlNode memberNode in memberNodes)
                {
                    string compartmentType = memberNode.Attributes["type"].Value;
                    string compartmentText = memberNode.InnerText;

                    if (compartmentType == "Field")
                    {
                        fieldCompartments.Add(compartmentText);
                    }
                    else if (compartmentType == "Method")
                    {
                        methodCompartments.Add(compartmentText);
                    }
                }
            }

            if (fieldCompartments.Count > 0)
            {
                compartments.Add(fieldCompartments);
            }

            if (methodCompartments.Count > 0)
            {
                compartments.Add(methodCompartments);
            }

            return compartments;
        }
    }

    class ClassShape
    {
        public string ClassName { get; }
        public string ShapeType { get; }

        public ClassShape(string className, string shapeType)
        {
            ClassName = className;
            ShapeType = shapeType;
        }
    }

    class BoxInfo
    {
        public string Name { get; set; } = string.Empty;
        public int Compartments { get; set; }
    }

    class BoxInfoCompartments
    {
        public string Name { get; set; } = string.Empty;
        public List<List<string>> CompartmentsInfo { get; set; } = new List<List<string>>();
    }

    public class ClassRelationship
    {
        public string FirstClass { get; set; } = string.Empty;
        public string SecondClass { get; set; } = string.Empty;
        public string RelationshipType { get; set; } = string.Empty;
        public string RelationshipSide { get; set; } = string.Empty;
        public string RelationshipDirection { get; set; } = string.Empty;
        public string AssociationType { get; set; } = string.Empty;
    }
}
