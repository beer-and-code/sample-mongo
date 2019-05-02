using System;

namespace SimpleMongo.Api.Models {
    public class Sample : MongoEntity {

        public Sample (string name, string description) {
            this.Name = name;
            this.Description = description;
            this.CreatedAt = DateTime.Now;
        }
        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime CreatedAt { get; private set; }

    }
}