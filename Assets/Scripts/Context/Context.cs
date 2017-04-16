using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour {

    public List<Unit> allplayerunits = new List<Unit>();
    public SelectionManager SelectionManager;
    public NavigationManager NavigationManager;
    void Awake()
    {
        
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        _context = this;

        SelectionManager = GetComponent<SelectionManager>();
        NavigationManager = GetComponent<NavigationManager>();
    }
    private static Context _context;
    public static Context Instance() { return _context; }
}
