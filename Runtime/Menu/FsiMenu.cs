using System;
using System.Collections.Generic;
using Fsi.General;
using UnityEngine;

namespace Fsi.Ui.Menu
{
	public class FsiMenu<T> : MbSingleton<FsiMenu<T>>
	{
		private readonly Stack<T> pageStack = new();
		private FsiPage<T> currentPage;
		
		private Dictionary<T, FsiPage<T>> _pages;
		private Dictionary<T, FsiPage<T>> Pages
		{
			get
			{
				_pages ??= BuildDictionary();
				return _pages;
			}
		}
		
		[Header("Pages")]

		[SerializeField]
		private T startPage;
		
		[SerializeField]
		private List<FsiPage<T>> pages = new();

		// protected override void Awake()
		// {
		// 	base.Awake();
		// 	foreach (FsiPage<T> page in Pages.Values)
		// 	{
		// 		page.gameObject.SetActive(false);
		// 	}
		// }
		
		protected override void Awake()
		{
			base.Awake();
			foreach (FsiPage<T> page in Pages.Values)
			{
				page.Initialize(this);
				page.gameObject.SetActive(false);
			}
		}

		protected virtual void Start()
		{
			GoToPage(startPage, null);
		}

		private void CloseCurrentPage(Action onComplete)
		{
			if (currentPage)
			{
				if (currentPage.ID.Equals(pageStack.Peek()))
				{
					pageStack.Pop();
				}

				currentPage.Close(onComplete);
			}
			else
			{
				onComplete?.Invoke();
			}
		}

		public void GoToPage(FsiPage<T> to, Action onComplete = null)
		{
			CloseCurrentPage(() =>
			                 {
				                 currentPage = to;
				                 
				                 pageStack.Push(currentPage.ID);
				                 currentPage.gameObject.SetActive(true);
				                 currentPage.Open(onComplete);
			                 });
		}

		// public void GoToPage(T to)
		// {
		// 	
		// }

		public void GoToPage(T to, Action onComplete = null)
		{
			Debug.Log($"Go to page {to}");
			if (!Pages.TryGetValue(to, out FsiPage<T> nextPage))
			{
				Debug.LogError($"Menu ({name}) could not find page of type {to}.", gameObject);
				return;
			}
			
			CloseCurrentPage(() =>
			                 {
				                 currentPage = nextPage;
				                 pageStack.Push(nextPage.ID);
				                 currentPage.gameObject.SetActive(true);
				                 currentPage.Open(() =>
				                                  {
					                                  Debug.Log("Current page opened.");
					                                  onComplete?.Invoke();
				                                  });
			                 });
		}

		private Dictionary<T, FsiPage<T>> BuildDictionary()
		{
			string s = "";

			Dictionary<T, FsiPage<T>> dict = new();
			for (int i = 0; i < pages.Count; i++)
			{
				FsiPage<T> page = pages[i];
				dict.Add(page.ID, page);
				s += $"Adding Page {page.ID}";
				if (i < pages.Count - 1) s += "\n";
			}

			Debug.Log($"Menu ({name}) indexed {pages.Count} pages.\n" +
			          $"{s}", gameObject);
			return dict;
		}
	}
}