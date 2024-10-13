using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UMLClassDiagramTranslator
{
    public class Field
    {
        public string? Name { get; set; }
    }

    public class Method
    {
        public string? Name { get; set; }
    }

    public class ClassModel
    {
        public string? ClassName { get; set; }
        public string? AccessModifier { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();
        public List<Method> Methods { get; set; } = new List<Method>();
    }
    internal class TranslateModel
    {
        static readonly XmlDocument xmlDoc = new XmlDocument();
        static XmlNodeList? classNodes; 
        static List<ClassRelationship> classRelationships = new List<ClassRelationship>();

        public static string TranslateToModel(string path)
        {
            StringBuilder sb = new StringBuilder();
            string filePath = path;

            try
            {
                xmlDoc.Load(filePath);
                classNodes = xmlDoc.SelectNodes("//Entity[@type='Class']");

                //get number of claases
                int classCount = GetClassCountFromXmlFile(filePath);

                //get class names
                string[] classNames = GetClassNamesFromXmlFile();
                string lastSeparator = "and " + classNames[classNames.Length - 1];

                string diagClasses =
                    String.Format("The uml class diagram has {0} classes, namely, {1}", 
                    classCount, String.Join(", ", classNames.ToArray(), 0, classNames.Length - 1)
                    + String.Format(", {0}", lastSeparator));
                

                sb.Append(diagClasses);
            }
            catch
            {
                MessageBox.Show("Please load file before translating!");
            }


            string classModelsDetails = ExtractClassModels();

            sb.Append(classModelsDetails);





            //computation of relationships
            try
            {
                // Load XML from file
                XDocument doc = XDocument.Load(filePath);

                // Extract entity names
                List<string> entityNames = doc.Descendants("Entity")
                    .Select(e => e.Element("Name")?.Value)
                    .ToList();

                // Create entity instances
                Dictionary<string, Entity> entities = new Dictionary<string, Entity>();
                foreach (string name in entityNames)
                {
                    Entity entity = new ClassEntity(name);
                    entities[name] = entity;
                }

                // Establish relationships
                foreach (XElement relationshipElement in doc.Descendants("Relationship"))
                {
                    string type = relationshipElement.Attribute("type")?.Value;
                    int firstIndex = int.Parse(relationshipElement.Attribute("first")?.Value);
                    int secondIndex = int.Parse(relationshipElement.Attribute("second")?.Value);

                    Entity firstEntity = entities[entityNames[firstIndex]];
                    Entity secondEntity = entities[entityNames[secondIndex]];

                    string associationType = relationshipElement.Element("AssociationType")?.Value; // Read associationType from the file

                    Relationship relationship = new Relationship(type, firstEntity, secondEntity, associationType);

                    firstEntity.AddRelationship(relationship);
                }

                // Print relationships
                sb.Append("\nClass relationships\n===================");/*add class relationships
                heading*/
                foreach (Entity entity in entities.Values)
                {
                    sb.Append(entity.PrintRelationships()); //adding relationships to output
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            return sb.ToString(); //placeholder
        }

        static int GetClassCountFromXmlFile(string filePath)
        {
            int classCount = 0;

            

            
            if (classNodes != null)
            {
                classCount = classNodes.Count;
            }

            return classCount;
        }

        static string[] GetClassNamesFromXmlFile()
        {
            string[] classNames;

            if (classNodes != null)
            {
                classNames = new string[classNodes.Count];

                for (int i = 0; i < classNodes.Count; i++)
                {
                    if(classNodes != null) { 
                    }
                    XmlNode? nameNode = classNodes[i]?.SelectSingleNode("Name");
                    if (nameNode != null)
                    {
                        classNames[i] = nameNode.InnerText;
                    }
                }
            }
            else
            {
                classNames = new string[0]; // Empty array if classNodes is null
            }

            return classNames;
        }

        public static string ExtractClassModels()
        {
            string modelDetails = String.Empty;

            // Find the <Entities> element and iterate over its child elements
            XmlNodeList? entityNodes = xmlDoc.SelectNodes("Project/ProjectItem/Entities/Entity");
            List<ClassModel> classModels = new List<ClassModel>();
            if (entityNodes != null)
            {
                foreach (XmlNode entityNode in entityNodes)
                {
                    string entityType = entityNode?.Attributes?["type"]?.Value??" ";
                    if (entityType == "Class")
                    {
                        string className = entityNode?.SelectSingleNode("Name")?.InnerText??" ";
                        string accessModifier = entityNode?.SelectSingleNode("Access")?.InnerText??" ";
                        ClassModel classModel = new ClassModel
                        {
                            ClassName = className,
                            AccessModifier = accessModifier
                        };

                        XmlNodeList? memberNodes = entityNode?.SelectNodes("Member");
                        if (memberNodes != null)
                        {
                            foreach (XmlNode memberNode in memberNodes)
                            {
                                string memberType = memberNode?.Attributes?["type"]?.Value??" ";
                                string memberName = memberNode?.InnerText??" ";

                                if (memberType == "Field")
                                {
                                    Field field = new Field
                                    {
                                        Name = memberName
                                    };
                                    classModel.Fields.Add(field);
                                }
                                else if (memberType == "Method")
                                {
                                    Method method = new Method
                                    {
                                        Name = memberName
                                    };
                                    classModel.Methods.Add(method);
                                }
                            }
                        }

                        classModels.Add(classModel);
                    }
                }
            }
            

            int classNo = 1;
            // Print the extracted class models
            foreach (ClassModel classModel in classModels)
            {
                modelDetails += $"\n{classNo}.Class Name: {classModel.ClassName}";
                modelDetails += $"\n{classModel.ClassName} class Access Modifier: {classModel.AccessModifier}";
                classNo++;

                if (classModel.Fields.Count > 0)
                {
                    modelDetails += $"\n{classModel.ClassName} class Fields:";
                    foreach (Field field in classModel.Fields)
                    {
                        modelDetails += $"\n{field.Name}";
                    }
                }

                if (classModel.Methods.Count > 0)
                {
                    modelDetails += $"\n{classModel.ClassName} class Methods:";
                    foreach (Method method in classModel.Methods)
                    {
                        modelDetails += $"\n{method.Name}";
                    }
                }

                modelDetails += "\n";
            }

            return modelDetails;
        }
    }

    public abstract class Entity
    {
        public string Name { get; set; }
        public List<Relationship> Relationships { get; }

        public Entity(string name)
        {
            Name = name;
            Relationships = new List<Relationship>();
        }

        public void AddRelationship(Relationship relationship)
        {
            Relationships.Add(relationship);
        }

        public virtual string PrintRelationships()
        {
            string output = String.Empty; //stores relationships' results
            
            if (Relationships.Count > 0)
            {
                output += $"\nRelationships of {Name}:";
                foreach (Relationship relationship in Relationships)
                {
                    string associationType = relationship.AssociationType != null ? $"{relationship.AssociationType}" : "";

                    output += $"\n-There is a {relationship.Type} relationship between {relationship.FirstEntity.Name} class and {relationship.SecondEntity.Name} class";
                    if (relationship.Type == "Generalization")
                        output += $"\n i.e. {relationship.FirstEntity.Name} class inherits from {relationship.SecondEntity.Name} class";
                    else
                        output += $"\n i.e. The association is of type {associationType}";

                    output += "\n";
                }
            }
            else
            {
                //Console.WriteLine($"No relationships for {Name}.");
            }

            return output;
        }
    }

    public class Relationship
    {
        public string Type { get; }
        public Entity FirstEntity { get; }
        public Entity SecondEntity { get; }
        public string AssociationType { get; } // Initialize AssociationType property

        public Relationship(string type, Entity firstEntity, Entity secondEntity, string associationType)
        {
            Type = type;
            FirstEntity = firstEntity;
            SecondEntity = secondEntity;
            AssociationType = associationType;
        }
    }

    public class ClassEntity : Entity
    {
        public ClassEntity(string name) : base(name)
        {
        }
    }

}
