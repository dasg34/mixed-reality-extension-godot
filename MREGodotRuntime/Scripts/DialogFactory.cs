// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using System.Collections;
using System.Collections.Generic;
using Godot;
using MixedRealityExtension.App;
using MixedRealityExtension.PluginInterfaces;
using System;

public class DialogFactory : Spatial, IDialogFactory
{
	//[SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
	[Export] private Assets.Scripts.User.InputSource inputSource;
	private ConfirmationDialog label;
	private TextEdit input;

	private class DialogQueueEntry
	{
		public string text;
		public bool allowInput;
		public Action<bool, string> callback;
	}

	private Queue<DialogQueueEntry> queue = new Queue<DialogQueueEntry>(3);
	private DialogQueueEntry activeDialog;

	public override void _Ready()
	{
		label = GetNode<ConfirmationDialog>("CanvasLayer/ConfirmationDialog");
		input = GetNode<TextEdit>("CanvasLayer/TextEdit");
		label.AnchorTop = 0.3f;
		input.RectSize = new Vector2(300, 300);
		HideDialog();
	}

	public void ShowDialog(IMixedRealityExtensionApp app, string text, bool acceptInput, Action<bool, string> callback)
	{
		queue.Enqueue(new DialogQueueEntry() { text = text, allowInput = acceptInput, callback = callback });
		ProcessQueue();
	}

	private void ProcessQueue()
	{
		if (queue.Count == 0) return;

		activeDialog = queue.Dequeue();
		label.DialogText = activeDialog.text;
		label.Popup_();
		if (activeDialog.allowInput)
		{
			label.DialogText += "\n\n";
			input.Visible = true;
		}
/*
		controller.enabled = false;
		inputSource.disa = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		*/
	}

	private void OnOk()
	{
		try
		{
			activeDialog.callback?.Invoke(true, activeDialog.allowInput ? input.Text : null);
		}
		catch (Exception e)
		{
			GD.PushError(e.ToString());
		}
		finally
		{
			activeDialog = null;
		}

		HideDialog();
	}

	private void OnCancel()
	{
		try
		{
			activeDialog.callback?.Invoke(false, null);
		}
		catch (Exception e)
		{
			GD.PushError(e.ToString());
		}
		finally
		{
			activeDialog = null;
		}

		HideDialog();
	}

	private void HideDialog()
	{
				/*
		controller.enabled = true;

		inputSource.enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
*/
		ProcessQueue();
	}
}
