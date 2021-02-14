using Amazon.DynamoDBv2.DataModel;
using System;

namespace Serials.Core
{
    [DynamoDBTable("serials")]
    public class Serials
    {
        [DynamoDBProperty("serialnumber")]
        [DynamoDBHashKey]
        public string SerialNumber { get; set; }

        [DynamoDBProperty("firstname")]
        public string Firstname { get; set; }

        [DynamoDBProperty("lastname")]
        public string Lastname { get; set; }

        [DynamoDBProperty("activationDate")]
        public string ActivationDate { get; set; }

        [DynamoDBProperty("activation")]
        public string Activation { get; set; }

        [DynamoDBProperty("email")]
        public string Email { get; set; }

        [DynamoDBProperty("info")]
        public string Info { get; set; }

        [DynamoDBProperty("software")]
        public string Software { get; set; }
    }
}


//irstname: String
//lastname: String
//activation: String
//activationDate: Number
//email: String
//info: String
//serialNumber: String
//software: String
