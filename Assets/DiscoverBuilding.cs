using UnityEngine;

public class DiscoverBuilding : MonoBehaviour
{
    public bool dissolve_mode;
    private float transition = 0f;
    private Material dissolve;
    private float dissolve_speed = 0.5f;

    // old
    public Material Hidden;
    public Material Discovered;
    //private GameObject ParticleDiscover;
    private bool isDiscovered;

    private void Start()
    {
        CapsuleCollider capsule = this.gameObject.AddComponent<CapsuleCollider>();
        capsule.isTrigger = true;
        capsule.center = new Vector3(0f,200f,0f);
        capsule.radius = 300;
        capsule.height = 1000;
        capsule.direction = 1;

        if (dissolve_mode)
            dissolve = GetComponent<MeshRenderer>().material;

        if (!Hidden) return;
        this.transform.GetComponent<MeshRenderer>().material = Hidden;
    }

    public void Discover()
    {
        if (isDiscovered) return;
        isDiscovered = true;

        if (!dissolve_mode)
            this.transform.GetComponent<MeshRenderer>().material = Discovered;
        //Instantiate(ParticleDiscover); - position?
    }

    public void Update()
    {
        // Dissolve effect
        if (!dissolve_mode) return;
        if (!isDiscovered) return;
        if (transition >= 1f) return;
        
        Debug.Log("DISSOLVE");
        
        transition += Time.deltaTime * dissolve_speed;
        dissolve.SetFloat("Vector1_7761F0F9", transition);
    }
}
