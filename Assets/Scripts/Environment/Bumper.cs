using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bounceStrength;

    public Animator animator;

    public List<Material> alienMaterialList;
    public Renderer alienRenderer;

    private Material currentMaterial;


    private void Start()
    {
        RandomizeMaterial();
    }

    private void OnCollisionEnter(Collision other) {
        Player player = other.gameObject.GetComponent<Player>();
        
        if (player) {
            Vector3 direction = player.transform.position - transform.position;
            player.AddForceImpulse(direction.normalized, bounceStrength);
            RandomizeMaterial();
            animator.SetTrigger("Bump");
        }
    }

    private void RandomizeMaterial()
    {
        List<Material> notDispalyedMaterials = alienMaterialList.Where(material => material != currentMaterial).ToList();
        Material newMaterial = notDispalyedMaterials[Random.Range(0, notDispalyedMaterials.Count)];
        currentMaterial = newMaterial;
        alienRenderer.material = currentMaterial;
    }
}
