public class InsertQuery : IQuery
{
    private string _statement;

    private string _value;

    private string _where;

    private Originator originator;

    private Caretaker caretaker;

    public void SetStatement(string tablename)
    {
        _statement = "INSERT INTO " + tablename;
    }
        
    public void SetValue(IDictionary<string, object> columnValues)
    {
        string COLUMN = " (";
        string VALUE = " (";
        int index = 0;
        foreach (string key in columnValues.Keys)
        {
            var value = columnValues[key];
            COLUMN += key;
            VALUE += value.toString();
            if(index == columnValues.Keys.Count - 1)
            {
                COLUMN +=") ";
                VALUE += ") ";
            }
            else 
            {
                COLUMN +=", ";
                VALUE += ", ";
            }
        }
        _value =  COLUMN + " VALUES " + VALUE;
    }
        
    public void SetWhere(IList<IPredicate> predicates)
    {
        _where = "";
    }

    public string toQueryString()
    {
        return _statement + " " + _value + ";";
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
}