﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace ICSharpCode.FormsDesigner.Services
{
	public class DesignerEventService : IDesignerEventService
	{
		IDesignerHost activeDesigner = null;
		List<IDesignerHost> designers = new List<IDesignerHost>();
		
		public void Reset()
		{
			this.activeDesigner = null;
			this.designers.Clear();
		}
		
		public IDesignerHost ActiveDesigner {
			get {
				return activeDesigner;
			}
		}
		
		public DesignerCollection Designers {
			get {
				return new DesignerCollection(designers);
			}
		}
		
		public void AddDesigner(IDesignerHost host) 
		{
			this.designers.Add(host);
			if (designers.Count == 1) {
				SetActiveDesigner(host);
			}
			OnDesignerCreated(new DesignerEventArgs(host));
		}
		
		public void RemoveDesigner(IDesignerHost host)
		{
			designers.Remove(host);
			if (activeDesigner == host) {
				if (designers.Count <= 0) {
					SetActiveDesigner(null);
				} else {
					this.SetActiveDesigner((IDesignerHost)this.designers[designers.Count - 1]);
				}
			}
			((IContainer)host).Dispose();
			OnDesignerDisposed(new DesignerEventArgs(host));
		}
		
		public void SetActiveDesigner(IDesignerHost host) 
		{
			if (activeDesigner != host) {
				IDesignerHost oldDesigner = activeDesigner;
				activeDesigner = host;
				FileSelectionChanged();
				OnActiveDesignerChanged(new ActiveDesignerEventArgs(oldDesigner, host));
			}
		}
		
		public void FileSelectionChanged()
		{
			if (SelectionChanged != null) {
				SelectionChanged(this, EventArgs.Empty);
			}
		}
		
		protected virtual void OnDesignerCreated(DesignerEventArgs e)
		{
			if (DesignerCreated != null) {
				DesignerCreated(this, e);
			}
		}
		
		protected virtual void OnDesignerDisposed(DesignerEventArgs e)
		{
			if (DesignerDisposed != null) {
				DesignerDisposed(this, e);
			}
		}
		
		protected virtual void OnActiveDesignerChanged(ActiveDesignerEventArgs e)
		{
			if (ActiveDesignerChanged != null) {
				ActiveDesignerChanged(this, e);
			}
		}
		
		public event EventHandler SelectionChanged;
		
		public event DesignerEventHandler DesignerCreated;
		public event DesignerEventHandler DesignerDisposed;
		
		public event ActiveDesignerEventHandler ActiveDesignerChanged;
		
	}
}
