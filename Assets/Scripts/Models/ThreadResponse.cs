using System.Collections.Generic;

namespace ThreadModels
{
    [System.Serializable]
    public class ThreadResponse
    {
        public string id;
        public string @object;
        public long created_at;
        public Metadata metadata;
    }
    
    [System.Serializable]
    public class Metadata
    {
        // Define metadata properties here
        // This example assumes an empty object since your JSON shows "metadata":{}
        // If metadata contains properties, they should be defined here similar to other classes
    }
}