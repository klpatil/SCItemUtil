using SCBasics.Kernel;
using Sitecore.ContentSearch.Maintenance;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Data.Proxies;
using Sitecore.SecurityModel;
using Sitecore.sitecore.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sc82basics.sitecore.admin
{
    
    public partial class ItemUtil : AdminPage
    {
        int successItemsCount;
        int ignoreItemsCount;

        protected override void OnInit(EventArgs e)
        {
            base.CheckSecurity(true); //Required!
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            
            Server.ScriptTimeout = int.MaxValue;
        }

        protected void btnStartProcessing_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    ShowMessage("Please make sure all required inputs are provided", "Once done, please try again");
                }
                else
                {
                    // Start processing
                    Database database = Sitecore.Configuration.Factory.GetDatabase("master");

                    if (database != null)
                    {
                        Item startItem = database.GetItem(txtStartPath.Text.Trim());

                        if (startItem != null)
                        {
                            Item sourceTemplateItem = database.GetItem(txtSourceTemplatePath.Text.Trim());
                            Item targetTemplateItem = database.GetItem(txtTargetTemplatePath.Text.Trim());

                            if (sourceTemplateItem != null || targetTemplateItem != null)
                            {
                                

                                StringBuilder sBuilder = new StringBuilder(string.Empty);
                                DateTime dtStart = DateTime.Now;

                                bool result = ChangeTemplate(startItem, sourceTemplateItem, targetTemplateItem, sBuilder, chkAllChildrens.Checked);                                

                                DateTime dtEnd = DateTime.Now;
                                TimeSpan ts = dtEnd - dtStart;
                                sBuilder.AppendLine("===================SUMMARY=========================");
                                sBuilder.AppendLine("Time Taken (MS): " + ts.TotalMilliseconds.ToString());
                                sBuilder.AppendLine("Operation completed successfuly. Total Items found " + (successItemsCount + ignoreItemsCount) + ".");
                                sBuilder.AppendLine("Successfully coverted items Count: " + successItemsCount);
                                sBuilder.AppendLine("Ignored items Count: " + ignoreItemsCount);
                                if (!string.IsNullOrEmpty(sBuilder.ToString()))
                                {
                                    txtProcessResult.Text =  sBuilder.ToString();
                                    // Log full details
                                    Sitecore.Diagnostics.Log.Info("Processing completed : " + sBuilder.ToString(), this);
                                }
                               
                            }
                            else
                            {
                                ShowMessage("Source/Target Template item not found", txtSourceTemplatePath.Text + "/" + txtTargetTemplatePath.Text);
                            }
                        }
                        else
                        {
                            ShowMessage("Start Path item not found", txtStartPath.Text);                            
                        }
                    }
                    else
                    {
                        ShowMessage("Database not found", "master");
                    }

                }


            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Start processing click", ex, this);
                ShowMessage("Something went wrong while processing. Please check logs for detaild information", ex.Message);
            }
        }
        
        private bool ChangeTemplate(Item startItem, Item sourceTemplateItem, Item targetTemplateItem, StringBuilder sBuilder, bool @checked)
        {
            bool result = false;
            if (chkFastandFurious.Checked)
            {
                // https://blog.horizontalintegration.com/2016/02/12/disablers-disablers-disablers-disablers-a-lesson-in-mass-sitecore-updates/
                using (new SecurityDisabler())
                using (new ProxyDisabler())
                using (new DatabaseCacheDisabler())
                using (new EventDisabler())
                using (new BulkUpdateContext())
                {
                    try
                    {
                        sBuilder.AppendLine("ChangeTemplate Started In Fast And Furious way");
                        // https://blog.krusen.dk/disable-indexing-temporarily-in-sitecore-7/
                        IndexCustodian.PauseIndexing();
                        ChangeTemplateExecute(startItem, sourceTemplateItem, targetTemplateItem, sBuilder);

                        if (@checked)
                        {
                            foreach (Item item in startItem.Children)
                            {
                                ChangeTemplateExecute(item, sourceTemplateItem, targetTemplateItem, sBuilder);
                            }
                        }
                    }
                    finally
                    {
                        IndexCustodian.ResumeIndexing();
                    }
                }
            }
            else
            {
                sBuilder.AppendLine("ChangeTemplate Started In Slow and Steady way");
                // Process Main Item
                ChangeTemplateExecute(startItem, sourceTemplateItem, targetTemplateItem, sBuilder);

                if (@checked)
                {
                    foreach (Item item in startItem.Children)
                    {
                        ChangeTemplateExecute(item, sourceTemplateItem, targetTemplateItem, sBuilder);
                    }
                }
            }
            
            result = true;
            return result;           
        }

        private void ChangeTemplateExecute(Item item, Item sourceTemplateItem, Item targetTemplateItem, StringBuilder sBuilder)
        {
            if (item.TemplateID == sourceTemplateItem.ID)
            {
                item.ChangeTemplate(targetTemplateItem);
                sBuilder.AppendFormat("* Template changed successfully for Item : {0} to Template : {1}.", item.Paths.Path,
                    targetTemplateItem.Paths.Path);
                sBuilder.AppendLine();
                successItemsCount++;
            }
            else
            {
                // Skipped as tempalte no match
                sBuilder.AppendFormat("* Template change skipped for Item : {0} to Template : {1} as No Template Match found.", item.Paths.Path,
                    targetTemplateItem.Paths.Path);
                sBuilder.AppendLine();
                ignoreItemsCount++;
            }
        }

        private bool ValidateInputs()
        {
            return string.IsNullOrWhiteSpace(txtStartPath.Text) || string.IsNullOrWhiteSpace(txtSourceTemplatePath.Text) || 
                string.IsNullOrWhiteSpace(txtTargetTemplatePath.Text);
        }

        private void ShowMessage(string message, string detailedmessage)
        {
            txtProcessResult.Text = string.Format("{0} - {1}", message, detailedmessage);
        }
    }
}