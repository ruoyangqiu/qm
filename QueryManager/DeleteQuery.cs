public class DeleteQuery : IQuery
{
    private string _statement;

    private string _value;

    private string _where;

    private Originator originator;

    private Caretaker caretaker;

    public void SetStatement(string tablename)
    {
        _statement = "DELETE FROM " + tablename;
    }
        
    public void SetValue(IDictionary<string, object> columnValues)
    {

        _value = "";
    }
        
    public void SetWhere(IList<IPredicate> predicates)
    {
        _where = WhereClause(predicates);
    }

    public string toQueryString()
    {
        return _statement + " " + _where + ";";
    }

    public int getState()
    {
        return originator.getState();
    }

    void setState(int state)
    {
        originator.setState(state);
    }

    void initializeMemento()
    {
        originator.setState(0);
        caretaker.setMemento(originator.createMemento());
    }

    private string WhereClause(IList<Ipredicate> predicates)
    {
        IDictionary<string, IList<Ipredicate>> where = new IDictionary<string, IList<Ipredicate>>();
        foreach(Ipredicate predicate in predicates)
        {
            if(!where.ContainsKey(predicates.ColumnName))
            {
                where.Add(predicates.ColumnName, new IList<Ipredicate>());
            }
            where[predicate.ColumnName].Add(predicate);
        }
        string wcq = "WHERE ";
        int index1 = 0;
        string condition = "";        
        foreach(string key in where.Keys)
        {

            condition += "(";
            int index2 = 0;
            foreach(Ipredicate p in where[key])
            {
                condition += parsePredicate(p);
                if(index2 < wherer[key].Count - 1)
                {
                    inStatement += " OR ";
                }
                index2 ++;
            }
            condition += ")";
            if(index1 < wherer.Keys.Count - 1)
                {
                    inStatement += " AND ";
                }
            index1 ++;
        }
        wcq +=  condition;
        returrn wcq;
    }

    private string parsePredicate(Ipredicate predicate)
    {
        switch(predicate.BinaryOperation)
        {
            case(BinaryOperation.Equals):
                return predicate.ColumnName + " = '" +  predicate.columnValues.toString() + "'";

            case(BinaryOperation.GreaterThan):
                return predicate.ColumnName + " > '" +  predicate.columnValues.toString() + "'";
            case(BinaryOperation.GreaterThan):
                return predicate.ColumnName + " > '" +  predicate.columnValues.toString() + "'";
            case(BinaryOperation.GreaterThanOrEquals):
                return predicate.ColumnName + " >= '" +  predicate.columnValues.toString() + "'";
            case(BinaryOperation.LessThan):
                return predicate.ColumnName + " < '" +  predicate.columnValues.toString() + "'";
            case(BinaryOperation.LessThanOrEquals):
                return predicate.ColumnName + " <= '" +  predicate.columnValues.toString() + "'";
            case(BinaryOperation.NotEquals):
                return "NOT " + predicate.ColumnName + " = '" +  predicate.columnValues.toString() + "'";
            case(BinaryOperation.Like):
                return predicate.ColumnName + " LIKE '" +  predicate.columnValues.toString() + "'";
            case(BinaryOperation.IsNull):
                return predicate.ColumnName + " IS NULL" ;
            case(BinaryOperation.In):
                string inStatement = "(";
                int index = 0;
                foreach(var o in predicate.columnValues)
                {
                    inStatement += o.toString();
                    if(index < predicate.columnValues.Count - 1)
                    {
                        inStatement += ", ";
                    }
                    index ++;
                }
                inStatement += ")";
                return predicate.ColumnName + " IN " + inStatement;
        }
    }
}