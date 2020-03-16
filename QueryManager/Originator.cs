public class Originator
{
    private int state;

    public void setState(int state)
    {
        this.state = state;
    }

    public int getState()
    {
        return state;
    }

    public Memento createMemento()
    {
        return new Memento(state);
    }

    public void restoreMemento(Memento m)
    {
        this.setState(m.getState());
    }

}