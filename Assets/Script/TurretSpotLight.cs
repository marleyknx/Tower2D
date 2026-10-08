using UnityEngine;
using UnityEngine.Rendering;

public class TurretSpotLight : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [SerializeField] int highlightOrder = 101;

    SortingGroup current;
    int originalOrder;

    public void Focus(TurretLeveler leveler)
    {
        // TODO 1 : si current n'est pas null → lui rendre son ordre d'origine
        if(current != null) current.sortingOrder = originalOrder;
        if(leveler == null)
        {
            current = null;
            overlay.SetActive(false);
            return;
        }
        
            var sortinGroup = leveler.GetComponent<SortingGroup>();
            current = sortinGroup;
            originalOrder = sortinGroup.sortingOrder;
            sortinGroup.sortingOrder = highlightOrder;
            overlay.SetActive(true);

        
        // TODO 2 : si leveler est null → cacher le voile, current = null, return
        // TODO 3 : récupérer le SortingGroup de la tourelle
        // TODO 4 : mémoriser son ordre actuel dans originalOrder
        // TODO 5 : lui donner highlightOrder + afficher le voile
    }
}
