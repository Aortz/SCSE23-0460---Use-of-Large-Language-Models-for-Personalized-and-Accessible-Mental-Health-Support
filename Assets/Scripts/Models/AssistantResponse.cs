using System.Collections.Generic;

namespace AssistantModel
{
    [System.Serializable]
    public class AssistantResponse
    {
        public string @object;
        public List<AssistantData> data;
        public string first_id;
        public string last_id;
        public bool has_more;
    }

    [System.Serializable]
    public class AssistantData
    {
        public string id;
        public string @object;
        public long created_at;
        public string name;
        public string description;
        public string model;
        public string instructions;
        public List<Tool> tools;
        public List<string> file_ids;
        public Metadata metadata;
    }

    [System.Serializable]
    public class Tool
    {
        public string type;
    }

    [System.Serializable]
    public class Metadata
    {
        // Define metadata properties here
        // This example assumes an empty object since your JSON shows "metadata":{}
        // If metadata contains properties, they should be defined here similar to other classes
    }
}