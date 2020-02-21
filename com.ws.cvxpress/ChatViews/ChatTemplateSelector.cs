using System;
using com.ws.cvxpress.Models;
using Xamarin.Forms;

namespace com.ws.cvxpress.ChatViews
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Travels_Conversations;
            if (messageVm == null)
                return null;


            return (messageVm.User_Sender == App.User) ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}
