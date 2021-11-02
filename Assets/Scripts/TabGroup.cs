
public interface ITabGroup  
{
    void Subscribe(TabButton button);
    void OnTabEnter(TabButton button);
    void OnTabExit(TabButton button);
    void OnTabSelected(TabButton button);
}
