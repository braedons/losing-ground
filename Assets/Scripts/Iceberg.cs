using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceberg : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;

	private IcebergManager icebergManager;

	public bool isMelted;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		icebergManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<IcebergManager>();
	}

	void Update()
	{

	}

	public IEnumerator Slide(Vector3 dir)
	{
		Vector3 targetPos = FindDestination(dir);
		float threshold = 0.1f;

		while (Vector3.Distance(transform.position, targetPos) > threshold)
		{
			transform.position += dir / 16;
			yield return new WaitForSecondsRealtime(.1f / icebergManager.slideSpeed);
		}
	}

	private Vector3 FindDestination(Vector3 dir)
	{
		RaycastHit2D hit = Physics2D.Raycast((transform.position), dir);

		if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Standable"))
		{
			return hit.transform.position - dir;
		}
		else
		{
			// Else it's a border
			return (Vector3)hit.point - (dir / 2);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == gameObject.layer)
		{
			rb.velocity = Vector3.zero;
		}
	}

	public void Melt(bool isMelted)
	{
		animator.SetBool("isMelted", isMelted);
		this.isMelted = isMelted;
	}

	// Pre-melt or pre-freeze, show highlight
	public void Pre(bool melting)
	{
		animator.SetTrigger(melting ? "premelt" : "prefreeze");
	}
}
