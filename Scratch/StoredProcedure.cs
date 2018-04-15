﻿using System.Collections.Generic;
using System.Data;

namespace Scratch
{
    public class StoredProcedure
    {
        public string Schema;
        public string Name;
        public string NameHumanCase;
        public List<StoredProcedureParameter> Parameters;
        public List<List<DataColumn>> ReturnModels; // A list of return models, containing a list of return columns
        public bool IsTVF;

        public StoredProcedure()
        {
            Parameters = new List<StoredProcedureParameter>();
            ReturnModels = new List<List<DataColumn>>();
        }

        public static bool IsNullable(DataColumn col)
        {
            return col.AllowDBNull &&
                   !(NotNullable.Contains(col.DataType.Name.ToLower())
                     || NotNullable.Contains(col.DataType.Namespace.ToLower() + "." + col.DataType.Name.ToLower()));
        }

        public static string WrapTypeIfNullable(string propertyType, DataColumn col)
        {
            if (!IsNullable(col))
                return propertyType;
            return string.Format(Settings.NullableShortHand ? "{0}?" : "System.Nullable<{0}>", propertyType);
        }
    }
}