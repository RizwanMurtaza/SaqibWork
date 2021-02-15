using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;

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
        public long? ActivationDate { get; set; }

        [DynamoDBProperty("activation")]
        public string Activation { get; set; }

        [DynamoDBProperty("email")]
        public string Email { get; set; }

        [DynamoDBProperty("info")]
        public string Info { get; set; }

        [DynamoDBProperty("software")]
        public string Software { get; set; }

        [DynamoDBProperty("language")]
        public string Language { get; set; }

        [DynamoDBProperty("notes")]
        public string Notes { get; set; }

        [DynamoDBProperty("activationHistory")]
        public List<ActivationHistoryItem> ActivationHistory { get; set; }

        [DynamoDBProperty("disabled")]
        public bool Disabled { get; set; }

        [DynamoDBProperty("deregistrationLimit")]
        public int? DeregistrationLimit { get; set; }
    }

    public class ActivationHistoryItem
    {
        [DynamoDBProperty("activation")]
        public string Activation { get; set; }

        [DynamoDBProperty("activationDate")]
        public string ActivationDate { get; set; }

        [DynamoDBProperty("activationInfo")]
        public string ActivationInfo { get; set; }

        [DynamoDBProperty("deactivationDate")]
        public string DeactivationDate { get; set; }

        [DynamoDBProperty("deactivationInfo")]
        public string DeactivationInfo { get; set; }
    }
}


//firstname: String
//lastname: String
//activation: String
//activationDate: Number
//email: String
//info: String
//serialNumber: String
//software: String
