using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace com.ws.cvxpress.ChatViews
{
    public partial class ChatIntpuBarView : ContentView
    {
        public ChatIntpuBarView()
        {
            InitializeComponent();
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            if (chatTextInput.Text == null || chatTextInput.Text.Length == 0) return;

            (this.Parent.Parent.BindingContext as ChatUIViewModel).OnSendCommand.Execute(null);
            chatTextInput.Text = "";
            chatTextInput.Focus();

            MessagingCenter.Send<ChatIntpuBarView, string>(this, "ScrollList", "Done");

        }
    }
}
