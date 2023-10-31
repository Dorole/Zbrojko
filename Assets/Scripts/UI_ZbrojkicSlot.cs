using UnityEngine;
using RPG.Core.UI.Dragging;

public class UI_ZbrojkicSlot : MonoBehaviour, IDragSource<SO_ZbrojkoItem>
{
    public SO_ZbrojkoItem GetItem()
    {
        //napravi class Zbrojkici
        //uvijek za GetItem vraca SO_Zbrojkic
        throw new System.NotImplementedException();
    }

    public int GetNumber()
    {
        //isto kao u UI_NumberSlot
        throw new System.NotImplementedException();
    }

    public void RemoveItems(int number)
    {
        //ovo bi trebalo zvati Zbrojkici.RemoveItem, eventualno za argument index slota
        //triggera se event na koji se pokrece animacija ulaska novog Zbrojkica u slot
        //pazi potencijalno ce biti problem ako se krene draggati, a onda se ipak vrati. 
        //event bi se trebao zvati tek kad je Zbrojkic sigurno spawnan u posudi! 
        throw new System.NotImplementedException();
    }

    //ne znam je li u biti potreban class Zbrojkici, mozda mogu sami handleati svoje iteme
    //jer svi imaju isti
}
