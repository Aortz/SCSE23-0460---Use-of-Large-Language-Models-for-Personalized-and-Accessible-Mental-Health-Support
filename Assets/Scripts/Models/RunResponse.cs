using System;
using System.Collections.Generic;

namespace RunModel
{
    [System.Serializable]
    public class RunResponse
    {
        public string id;
        public string @object; // Use @ to allow using reserved keyword 'object'
        public long created_at;
        public string assistant_id;
        public string thread_id;
        public string status;
        public long? started_at; // Nullable to accommodate null values
        public long? expires_at; // Nullable
        public long? cancelled_at; // Nullable
        public long? failed_at; // Nullable
        public long? completed_at; // Nullable
        public string last_error; // Nullable
        public string model;
        public string instructions; // Nullable
        public List<Tool> tools;
        public List<string> file_ids;
        public Metadata metadata;
        public Usage usage; // Assuming 'usage' is an object, create a Usage class if it has specific fields
    }

    public class Tool
    {
        public string type;
    }

    public class Metadata
    {
        // Assuming 'metadata' is an empty object, it's represented as an empty class.
        // If 'metadata' contains specific fields, they should be defined here.
    }

    public class Usage
    {
        // Define properties for 'usage' based on its structure in the JSON.
        // Since 'usage' is null in the provided JSON, it's unclear what fields it might contain.
    }
}