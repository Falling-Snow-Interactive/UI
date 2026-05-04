using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fsi.Ui.Menu
{
	public abstract class FsiPage<T> : MonoBehaviour
		// where T : Enum
	{
		public FsiMenu<T> Menu { get; private set; }
		
		private const float MaxAnimTime = 2f;

		public event Action Opened;
		public event Action Closed;
		
		public abstract T ID { get; }

		[Header("Animation")]

		[SerializeField]
		private Animator animator;
		
		[Space(5)]

		[SerializeField]
		private string openAnim = "Open";

		[SerializeField]
		private string closeAnim = "Close";
		
		[Header("References")]

		[SerializeField]
		private GameObject openSelect;

		// State properties
		public bool IsOpen => gameObject.activeSelf;
		public bool IsActive { get; private set; }

		public void Initialize(FsiMenu<T> menu)
		{
			Menu = menu;
		}
		
		#region Open

		// ReSharper disable Unity.PerformanceAnalysis
		/// <summary>
		/// Open the page.
		/// </summary>
		public virtual void Open(Action onComplete)
		{
			Debug.Log($"Opening page {name}");
			
			if (animator)
			{
				animator.SetTrigger(openAnim);
				StartCoroutine(WaitForAnim(() =>
				                           {
					                           OnOpened(onComplete);
				                           }));
			}
			else
			{
				OnOpened(onComplete);
			}
		}

		// ReSharper disable Unity.PerformanceAnalysis
		private void OnOpened(Action onComplete)
		{
			IsActive = true;
				
			EventSystem.current.SetSelectedGameObject(openSelect);
				
			onComplete?.Invoke();
			Opened?.Invoke();
		}
		
		#endregion
		
		#region Close

		/// <summary>
		/// Close the page.
		/// </summary>
		public virtual void Close(Action onComplete)
		{
			if (animator)
			{
				animator?.SetTrigger(closeAnim);

				StartCoroutine(WaitForAnim(() =>
				                           {
					                           OnClosed(onComplete);
				                           }));
			}
			else
			{
				OnClosed(onComplete);
			}
		}

		private void OnClosed(Action onComplete)
		{
			onComplete?.Invoke();
			Closed?.Invoke();
			gameObject.SetActive(false);
		}
		
		#endregion
		
		#region Utility

		private IEnumerator WaitForAnim(Action onComplete)
		{
			// Wait until we're actually in the state (important!)
			yield return null;

			float timer = MaxAnimTime;
			while (timer > 0)
			{
				AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
				if (state.normalizedTime >= 1f && !animator.IsInTransition(0))
				{
					break;
				}

				timer -= Time.unscaledDeltaTime;

				yield return null;
			}

			onComplete?.Invoke();
		}
		
		#endregion
	}
}