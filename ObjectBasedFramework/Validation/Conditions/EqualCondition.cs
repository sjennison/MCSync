﻿using ClussPro.ObjectBasedFramework.Schema;

namespace ClussPro.ObjectBasedFramework.Validation.Conditions
{
    public class EqualCondition : Condition
    {
        public EqualCondition() : base() { }

        public EqualCondition(string field, object value)
        {
            Field = field;
            Value = value;
        }

        public string Field { get; set; }
        public object Value { get; set; }

        public override bool Evaluate(DataObject dataObject)
        {
            SchemaObject schemaObject = Schema.Schema.GetSchemaObject(dataObject.GetType());
            Field field = schemaObject.GetField(Field);

            if (Value == null)
            {
                return field.GetValue(dataObject) == null;
            }
            else
            {
                return Value.Equals(field.GetValue(dataObject));
            }
        }
    }
}
