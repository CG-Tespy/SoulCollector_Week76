using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note to self: Perhaps tefine this script's functionality at some point.

/// <summary>
/// Applies a force to objects that contact the one this is attached to.
/// </summary>
public class ForceOnContact2D : MonoBehaviour2D
{
	[SerializeField] bool _useContactTags = 				false;
	[SerializeField] bool _useContactLayers = 				true;

	[Tooltip("Apply the force only to objects with these tags.")]
	[SerializeField] List<string> contactTags;
	[Tooltip("Apply the force only to objects within these tags.")]
	[SerializeField] LayerMask _contactLayers;

	[SerializeField] bool _respondToCollision = 			true;
	[SerializeField] bool _respondToTrigger = 				true;

	[SerializeField] Vector2 _forceToApply;

	protected override void OnCollisionEnter2D(Collision2D other)
	{
		base.OnCollisionEnter2D(other);

		Rigidbody2D otherRb = 		other.gameObject.GetComponent<Rigidbody2D>();

		if (!otherRb || !_respondToCollision)
			return;

		ConsiderApplyingForce(otherRb);
	}

	protected override void OnTriggerEnter2D(Collider2D otherCollider)
	{
		base.OnTriggerEnter2D(otherCollider);

		Rigidbody2D otherRb = 		otherCollider.gameObject.GetComponent<Rigidbody2D>();

		if (!otherRb || !_respondToTrigger)
			return;

		ConsiderApplyingForce(otherRb);
	}

	void ConsiderApplyingForce(Rigidbody2D otherRb)
	{
		bool applyForce = 			(_useContactTags && contactTags.Contains(otherRb.tag)) ||
									(_useContactLayers && _contactLayers.Contains(otherRb.gameObject.layer));
		
		if (applyForce)
			otherRb.AddForce(_forceToApply);
	}
}
