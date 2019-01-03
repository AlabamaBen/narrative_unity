using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DataObject {

    [SerializeField]
    private string projectTitle;
    [SerializeField]
    private string projectDescription;

    // Use this for initialization
    public DataObject(string projectTitle,string projectDescription) {
        this.projectTitle = projectTitle;
        this.projectDescription = projectDescription;
	}

    public override string ToString()
    {
        return this.projectTitle + " // " + projectDescription;
    }
}
