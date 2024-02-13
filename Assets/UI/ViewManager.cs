using UnityEngine;

public sealed class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }

    [SerializeField]
    private View[] views;

    [SerializeField]
    private View defalutView;

    private View _currentView;

    [SerializeField]
    private bool autoInitialize;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (autoInitialize) Initialize();
    }

    public void Initialize()
    {
        foreach (View view in views)
        {
            view.Initialize();

            view.Hide();
        }

        if (defalutView != null) Show(defalutView);
    }

    public void Show<TView>(object args = null) where TView : View
    {
        foreach (View view in views)
        {
            if (view is not TView) continue;
            
            if (_currentView != null) _currentView.Hide();

            view.Show(args);

            _currentView = view;

            break;
        }
    }

    public void Show(View view, object args = null)
    {
        if (_currentView != null) _currentView.Hide(); 
        
        view.Show(args);

        _currentView = view;
    }
}
