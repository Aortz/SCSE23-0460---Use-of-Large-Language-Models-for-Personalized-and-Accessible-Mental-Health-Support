using System;
using System.Collections.Generic;

namespace MessageModel
{
    [System.Serializable]
    public class MessageResponse
    {
        public string id;
        public string @object; // Use @ to allow using reserved keyword
        public long created_at;
        public string thread_id;
        public string role;
        public List<Content> content;
        public List<object> file_ids; // Assuming file_ids to be a list of some object types
        public object assistant_id; // null in the given JSON, so type is object
        public object run_id; // null in the given JSON, so type is object
        public Metadata metadata;
    }

    [System.Serializable]
    public class Content
    {
        public string type;
        public Text text;
    }

    [System.Serializable]
    public class Text
    {
        public string value;
        public List<object> annotations; // Assuming annotations to be a list of some object types
    }

    [System.Serializable]
    public class Metadata
    {
        // Define properties for Metadata if any
        // Empty object in the given JSON, so no properties are defined here
    }
}