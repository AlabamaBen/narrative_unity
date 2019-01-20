using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DataObject {

    public int sequenceId;
    public string character;
    public string dialogue;
    public string state;
    public bool skipable;
    public string delay;
    public string imgName;

    // Use this for initialization
    public DataObject(string _sequenceId, string _character, string _dialogue, string _state, string _skipable, string _delay, string _imgName) {
        sequenceId = int.Parse(_sequenceId);
        character = _character;
        dialogue = _dialogue;
        state = _state;
        bool skipped = (int.Parse(_skipable) == 0) ? false : true;
        skipable = skipped;
        delay = _delay;
        imgName = _imgName;
    }

    public override string ToString()
    {
        return character + "  : " + dialogue;
    }
}
