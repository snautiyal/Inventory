using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.master
{
    public partial class productmst : System.Web.UI.Page
    {
        string compaid1 = "";
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        // CommonFunction check = new CommonFunction();


        protected void Page_Load(object sender, EventArgs e)
        {


           // SVH.RefralURL = "~/masters/branches.aspx";
            //if (!SVH.IsUserLogined.Equals("Yes"))
            //    Response.Redirect("~/admin/login.aspx");


            if (Page.IsPostBack)
            {
            }
            else
            {
                //http://forums.asp.net/t/1515775.aspx
                //tbxBranchName.Attributes.Add("OnKeyPress", "return validateNumber(event)");
                //tbxBranchName.Attributes.Add("onkeyup", "return ismaxlength(this,8)");


                btnAddBranch.Enabled = true;
                btnModifyBranch.Enabled = true;
                btndeleteBranch.Enabled = true;

                string tmpStr = "SELECT CompAID,CompName FROM tblCompany WHERE (OurClientID=" + SVH.ClientID + ") ORDER By CompName";
                MyLocal.NewFillDDL(MyLocal.GetConnString(""), tmpStr, "CompName", "CompAID", ddlCompanyName);
                MyLocal.NewFillDDL(MyLocal.GetConnString(""), tmpStr, "CompName", "CompAID", ddlsrchCompName);

                tmpStr = "Select StateAID,StateName FROM tblStates ORDER By StateName";
                MyLocal.NewFillDDL(MyLocal.GetConnString(""), tmpStr, "StateName", "StateAID", ddlBranchState);


                //If Callied From Booking (Start)
                string msqs = Request.RawUrl;
                msqs = (msqs.Contains("?")) ? msqs.Substring(msqs.IndexOf('?') + 1) : "";
                lblPassedCompID.Text = "";
                //lblPassedCompID.Text = MyLocal.GetAnyValFromURL(msqs, "compid");
                if (!(string.IsNullOrEmpty(lblPassedCompID.Text)))
                {
                    if (lblPassedCompID.Text.Trim().Equals("error"))
                    {
                        lblfrommassage.Text = "Some Error in URL \\n Please Press Back Space \\nand go Back to Previous Page\\n";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + lblfrommassage.Text.Replace("'", "\'") + "');</script>");
                        lblPassedCompID.Text = "";
                    }
                    else
                    {
                        AddButtonProcess();
                        ddlCompanyName.SelectedIndex = ddlCompanyName.Items.IndexOf(ddlCompanyName.Items.FindByValue(lblPassedCompID.Text));
                    }
                }



            }


        }


        private string makeqstr()
        {

            String mQueryForSearch = "";
            int mNoofCondition = 0;

            if (tbxSearchBranchName.Text.Length > 0)
            {
                mQueryForSearch = mQueryForSearch + " (BrName  like '%" + tbxSearchBranchName.Text + "%') ";
                mNoofCondition = mNoofCondition + 1;
            }

            //if (ddlsrchCompName.Text.Trim().Length > 0)
            if (!(ddlsrchCompName.Text.Trim().ToLower().Equals("-- select --")))
            {
                if (mNoofCondition > 0)
                {
                    mQueryForSearch = mQueryForSearch + ((cbxCheck.Checked) ? " AND " : " OR ");

                }
                mQueryForSearch = mQueryForSearch + " (BCompID  = " + ddlsrchCompName.SelectedValue + ") ";
                mNoofCondition = mNoofCondition + 1;
            }



            if (mQueryForSearch.Length > 0)
            {
                mQueryForSearch = "SELECT B.BrAID,B.BCompID,B.BrName,B.BrAddress,B.BrCity,B.BrStateID,B.BrPinNo,C.CompName,S.StateName FROM tblBranch AS B LEFT OUTER JOIN tblCompany AS C ON B.BCompID = C.CompAID LEFT OUTER JOIN tblStates AS S ON B.BrStateID = S.StateAID WHERE " + mQueryForSearch + " AND (B.OurClientID=" + SVH.ClientID + ")";

            }
            else
            {
                mQueryForSearch = "SELECT B.BrAID,B.BCompID,B.BrName,B.BrAddress,B.BrCity,B.BrStateID,B.BrPinNo,C.CompName,S.StateName FROM tblBranch AS B LEFT OUTER JOIN tblCompany AS C ON B.BCompID = C.CompAID LEFT OUTER JOIN tblStates AS S ON B.BrStateID = S.StateAID WHERE (B.OurClientID=" + SVH.ClientID + ")";
            }
            return mQueryForSearch;

        }

        protected void SearchDate()
        {
            lblSearchmsg.Text = "";
            grdSearchedBranch.DataSource = DelvedW.Common.SearchResult(MyLocal.GetConnString(""), "tblBranch", makeqstr());

            grdSearchedBranch.DataBind();
            if (grdSearchedBranch.Rows.Count <= 0)
            {
                lblSearchmsg.Text = "No Records Found ! Please Search Again.";
            }

            //  ClearEntry();
            lblfrommassage.Text = "";
            Branchentry.Visible = false;



            btnAddUpdDelBranch.Visible = true;
            btnAddUpdDelBranch.Enabled = true;
            divdelconf.Visible = false;
        }
        protected void btnsearchBranch_Click(object sender, EventArgs e)
        {
            SearchDate();
        }

        private void ClearEntry()
        {
            tbxSearchBranchName.Text = "";
            tbxBranchName.Text = "";
            tbxAdress.Text = "";
            ddlBranchState.Text = "-- Select --";
            ddlCompanyName.Text = "-- Select --";
            tbxPinNo.Text = "";
            tbxCity.Text = "";

        }

        private void EnableDisableEntry(bool _TrueorFalase)
        {
            tbxBranchName.Enabled = _TrueorFalase;
            tbxAdress.Enabled = _TrueorFalase;
            tbxCity.Enabled = _TrueorFalase;
            ddlBranchState.Enabled = _TrueorFalase;
            tbxPinNo.Enabled = _TrueorFalase;





        }



        protected void btnAddUpdDelBranch_Click(object sender, EventArgs e)
        {

            if (!btnAddUpdDelBranch.Text.Contains("Delete"))
            {
                if (ddlCompanyName.Text.Length > 0)
                {
                    lblfrommassage.Text = "";
                }
                else
                {
                    lblfrommassage.Text = "Company ID is required";
                }
                if (tbxBranchName.Text.Length > 0)
                {
                    lblfrommassage.Text = "";
                }
                else
                {
                    lblfrommassage.Text = "Branch Name is required";
                }

                if (!(lblfrommassage.Text.Length > 0))
                {
                    if (tbxAdress.Text.Length > 0)
                    {
                        lblfrommassage.Text = "";
                    }
                    else
                    {
                        lblfrommassage.Text = "Branch Address is required";
                    }

                }

                if (!(lblfrommassage.Text.Length > 0))
                {
                    if (tbxCity.Text.Length > 0)
                    {
                        lblfrommassage.Text = "";
                    }
                    else
                    {
                        lblfrommassage.Text = "Branch City is required";
                    }

                }
                if (!(lblfrommassage.Text.Length > 0))
                {
                    if (ddlBranchState.Text.Length > 0)
                    {
                        lblfrommassage.Text = "";
                    }
                    else
                    {
                        lblfrommassage.Text = "Branch State is required";
                    }
                }

                if (!(lblfrommassage.Text.Length > 0))
                {
                    if (tbxPinNo.Text.Length > 0)
                    {
                        lblfrommassage.Text = "";
                    }
                    else
                    {
                        lblfrommassage.Text = "Pin No. is required";
                    }
                }




                string mBrCompId = DelvedW.Common.GetValueFromTbl(MyLocal.GetConnString(""), "tblCompany", "LOWER(CompName)='" + ddlCompanyName.Text.ToLower() + "'", "CompAID");
                if (!(lblfrommassage.Text.Length > 0))
                {
                    string msDupChkStr;
                    if (btnAddUpdDelBranch.Text.Contains("Save"))
                    {
                        msDupChkStr = "LOWER(BrName)='" + tbxBranchName.Text.ToLower() + "' AND BCompId=" + mBrCompId + " AND (OurClientID=" + SVH.ClientID + ")";
                    }
                    else
                    {
                        msDupChkStr = "NOT BrAID=" + lblBranchAID.Text + " AND LOWER(BrName)='" + tbxBranchName.Text.ToLower() + "' AND BCompId=" + mBrCompId + " AND (OurClientID=" + SVH.ClientID + ")";
                    }


                    if (DelvedW.Common.IsExist(MyLocal.GetConnString(""), "tblBranch", msDupChkStr))
                    {
                        lblfrommassage.Text = "Branch Name " + tbxBranchName.Text + " is allready Existing";
                    }
                    else
                    {
                        lblfrommassage.Text = "";
                    }
                }



                if (btnAddUpdDelBranch.Text.Contains("Update") && (compaid1.Length > 0))
                {
                    lblfrommassage.Text = "Record ID is Missing";
                }
                if (!(lblfrommassage.Text.Length > 0)) // = "Teacher"
                {
                    if (btnAddUpdDelBranch.Text.Contains("Save") || (btnAddUpdDelBranch.Text.Contains("Update")))
                    {

                        SqlConnection con = new SqlConnection(MyLocal.GetConnString(""));

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = btnAddUpdDelBranch.Text.Contains("Save") ? "sp_AddBranch" : "sp_UpdBranch";
                        //if (btnAddUpdDelBranch.Text.Contains("Update") && (mguestAID4UnD.Length > 0))
                        if (btnAddUpdDelBranch.Text.Contains("Update"))
                        {
                            cmd.Parameters.Add("@braid", SqlDbType.Int).Value = lblBranchAID.Text; // mguestAID4UnD;
                            cmd.Parameters.Add("@uby", SqlDbType.NVarChar).Value = SVH.LoginedID;
                            cmd.Parameters.Add("@udt", SqlDbType.DateTime).Value = DateTime.Now.ToString();
                        }
                        else
                        {
                            cmd.Parameters.Add("@sessionid", SqlDbType.NVarChar).Value = SVH.SSessionID;
                            cmd.Parameters.Add("@urip", SqlDbType.NVarChar).Value = SVH.ClientIP;
                            cmd.Parameters.Add("@ourclientid", SqlDbType.NVarChar).Value = SVH.ClientID;
                            cmd.Parameters.Add("@isdeleted", SqlDbType.NVarChar).Value = "0";
                            cmd.Parameters.Add("@isactive", SqlDbType.NVarChar).Value = "1";
                            cmd.Parameters.Add("@aby", SqlDbType.NVarChar).Value = SVH.LoginedID;
                            cmd.Parameters.Add("@adt", SqlDbType.DateTime).Value = DateTime.Now.ToString();

                        }
                        //cmd.Parameters.Add("@bCompid", SqlDbType.NVarChar).Value = mBrCompId;
                        cmd.Parameters.Add("@bCompid", SqlDbType.NVarChar).Value = ddlCompanyName.Text.Equals("-- Select --") ? "0" : ddlCompanyName.SelectedValue.ToString();

                        cmd.Parameters.Add("@brname", SqlDbType.NVarChar).Value = tbxBranchName.Text;
                        cmd.Parameters.Add("@braddress", SqlDbType.NVarChar).Value = tbxAdress.Text;
                        cmd.Parameters.Add("@brcity", SqlDbType.NVarChar).Value = tbxCity.Text;
                        //cmd.Parameters.Add("@brstateid", SqlDbType.Int).Value = DelvedW.Common.GetValueFromTbl(MyLocal.GetConnString(""), "tblStates", "LOWER(StateName)='" + ddlBranchState.Text.ToLower() + "'", "StateAID");
                        cmd.Parameters.Add("@brstateid", SqlDbType.Int).Value = ddlBranchState.Text.Equals("-- Select --") ? "0" : ddlBranchState.SelectedValue.ToString();
                        cmd.Parameters.Add("@brpinno", SqlDbType.NVarChar).Value = tbxPinNo.Text;

                        cmd.Connection = con;


                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            lblfrommassage.Text = "Record for Branch =" + tbxBranchName.Text + " is successfully ";
                            if (btnAddUpdDelBranch.Text.Contains("Save"))
                            {
                                lblfrommassage.Text = lblfrommassage.Text + "Inserted..";
                            }
                            else
                            {
                                lblfrommassage.Text = lblfrommassage.Text + "updated..";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                            // lblfrommassage.Text = "Not Inserted..";
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();

                        }



                        btnAddBranch.Enabled = true;
                        ClearEntry();
                    }
                    else if (btnAddUpdDelBranch.Text.Contains("Update"))
                    {



                        if (tbxBranchName.Text.Length > 0)
                        {
                            // obj4modStudent.ModifyStudentLogin();

                        }
                        // lblfrommassage.Text = obj4modStudent.StatusMessage;
                        grdSearchedBranch.DataSource = DelvedW.Common.SearchResult(MyLocal.GetConnString(""), "tblBranch", makeqstr());
                        grdSearchedBranch.DataBind();


                        Branchentry.Visible = false;
                        btnModifyBranch.Enabled = true;
                        //ClearEntry();
                    }


                }

            }
            else if (btnAddUpdDelBranch.Text.Contains("Delete"))
            {
                divdelconf.Visible = true;
                btnAddUpdDelBranch.Visible = false;
                BtnCancel.Visible = false;

            }
        }

        //private bool CheckCompName()
        //{
        //    bool mRetVal = false;
        //    DataSet ds = new DataSet();
        //    DataTable dt = null;
        //    SqlDataAdapter da = default(SqlDataAdapter);
        //    SqlConnection con = new SqlConnection(MyLocal.GetConnString(""));
        //    string mSQLST = "";
        //    if (btnAddUpdDelBranch.Text.Contains("Save"))
        //    {
        //        mSQLST = "SELECT BrName FROM tblBranch WHERE LOWER(BrName)='" + tbxBranchName.Text.ToLower() + "' AND (OurClientID=" + SVH.ClientID + ")";
        //    }
        //    else
        //    {
        //        mSQLST = "SELECT BrName FROM tblBranch WHERE NOT BrAID=" + lblBranchAID.Text + " AND LOWER(BrName)='" + tbxBranchName.Text.ToLower() + "' AND (OurClientID=" + SVH.ClientID + ")";
        //    }

        //    da = new SqlDataAdapter(mSQLST, con);
        //    da.Fill(ds, "tblBranch");
        //    dt = ds.Tables["tblBranch"];

        //    if (dt.Rows.Count > 0)
        //    {
        //        mRetVal = false;
        //    }
        //    else
        //    {
        //        mRetVal = true;
        //    }
        //    return mRetVal;
        //}


        protected void btnBackToMain_Click(object sender, EventArgs e)
        {
            btnAddBranch.Enabled = true;
            btnAddBranch.Visible = true;
            btnBackToMain.Visible = false;
            btnModifyBranch.Enabled = true;
            btnModifyBranch.Visible = true;
            btndeleteBranch.Enabled = true;
            btndeleteBranch.Visible = true;
            divwelcome.Visible = true;
            searchBranch.Visible = false;
            Branchentry.Visible = false;
            ClearEntry();
            lblPageHeading.Text = "Branch Page";
            grdSearchedBranch.DataSource = null;
            grdSearchedBranch.DataBind();
            divdelconf.Visible = false;
            lblfrommassage.Text = "";
            btnAddUpdDelBranch.Visible = true;
            btnAddUpdDelBranch.Enabled = true;

        }




        protected void btnAddBranch_Click(object sender, EventArgs e)
        {
            AddButtonProcess();
        }

        private void AddButtonProcess()
        {
            btnBackToMain.Visible = true;
            btnAddBranch.Visible = false;
            btnAddBranch.Enabled = false;
            btnModifyBranch.Visible = false;
            btndeleteBranch.Visible = false;
            searchBranch.Visible = false;
            EnableDisableEntry(true);
            btnAddUpdDelBranch.Text = "Save";
            ClearEntry();
            btnAddUpdDelBranch.Enabled = true;
            divwelcome.Visible = false;
            Branchentry.Visible = true;
            lblPageHeading.Text = "Branch Addition Page";
            // filltbxBranchName();

        }

        //protected void MyLocal.ValidateRights();  //preventoperation()
        //{
        //    if (SVH.UserType.ToLower().Equals("12") || SVH.UserType.ToLower().Equals("11") || SVH.UserType.ToLower().Equals("00"))
        //    {
        //    }
        //    else
        //    {
        //        SVH.Msg1 = "You Are Not Autherised To Do This Operation";
        //        Response.Redirect("/message.aspx");
        //    }
        //}

        protected void btnModifyBranch_Click(object sender, EventArgs e)
        {
            MyLocal.ValidateRights();  //preventoperation();
            btnBackToMain.Visible = true;
            btnAddBranch.Visible = false;
            btnAddBranch.Enabled = false;
            btnModifyBranch.Visible = false;
            btnModifyBranch.Enabled = false;
            btndeleteBranch.Visible = false;
            btndeleteBranch.Enabled = false;
            searchBranch.Visible = true;
            ClearEntry();
            btnAddUpdDelBranch.Text = "Update";
            btnAddUpdDelBranch.Enabled = false;
            lblPageHeading.Text = "Branch Modification Page";




        }

        protected void btndeleteBranch_Click(object sender, EventArgs e)
        {
            MyLocal.ValidateRights();  //preventoperation();
            btnBackToMain.Visible = true;
            btnAddBranch.Visible = false;
            btnAddBranch.Enabled = false;
            btnModifyBranch.Visible = false;
            btnModifyBranch.Enabled = false;
            btndeleteBranch.Visible = false;
            btndeleteBranch.Enabled = false;
            divwelcome.Visible = false;
            searchBranch.Visible = true;
            ClearEntry();
            btnAddUpdDelBranch.Text = "Delete";
            btnAddUpdDelBranch.Enabled = false;
            lblPageHeading.Text = "Branch Deletion Page";



        }



        public void GetBranchRecordValues(String _BranchrecID)
        {
            DataSet ds = new DataSet();
            DataTable dt = null;
            SqlDataAdapter da = default(SqlDataAdapter);
            SqlConnection con = new SqlConnection(MyLocal.GetConnString(""));
            string mSQLSTR = "SELECT B.BrAID,B.BCompID,B.BrName,B.BrAddress,B.BrCity,B.BrStateID,B.BrPinNo,C.CompName,S.StateName FROM tblBranch AS B LEFT OUTER JOIN tblCompany AS C ON B.BCompID = C.CompAID LEFT OUTER JOIN tblStates AS S ON B.BrStateID = S.StateAID WHERE B.BrAID=" + _BranchrecID + " AND (B.OurClientID=" + SVH.ClientID + ")";
            mSQLSTR = "SELECT * FROM tblBranch WHERE BrAID=" + _BranchrecID + " AND (OurClientID=" + SVH.ClientID + ")";
            da = new SqlDataAdapter(mSQLSTR, con);
            da.Fill(ds, "tblBranch");
            dt = ds.Tables["tblBranch"];

            if (dt.Rows.Count > 0)
            {
                ddlCompanyName.SelectedIndex = ddlCompanyName.Items.IndexOf(ddlCompanyName.Items.FindByValue(dt.Rows[0]["BCompID"].ToString()));
                //ddlCompanyName.Text = dt.Rows[0]["CompName"].ToString();
                tbxBranchName.Text = dt.Rows[0]["BrName"].ToString();
                tbxAdress.Text = dt.Rows[0]["BrAddress"].ToString();
                tbxCity.Text = dt.Rows[0]["BrCity"].ToString();
                //ddlBranchState.Text = dt.Rows[0]["StateName"].ToString();
                ddlBranchState.SelectedIndex = ddlBranchState.Items.IndexOf(ddlBranchState.Items.FindByValue(dt.Rows[0]["BrStateID"].ToString()));
                tbxPinNo.Text = dt.Rows[0]["BrPinNo"].ToString();

            }
        }






        protected void btnDelConfYes_Click(object sender, EventArgs e)
        {
            if (CanItBeDeleted().Equals("Yes"))
            {
                SqlConnection con = new SqlConnection(MyLocal.GetConnString(""));

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_DelBranch";
                cmd.Parameters.Add("@Braid", SqlDbType.NVarChar).Value = lblBranchAID.Text; ;
                cmd.Connection = con;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblfrommassage.Text = "Record Deleted successfully";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    con.Dispose();

                }
                divdelconf.Visible = false;

                //Student obj4delStudent = new Student();

                grdSearchedBranch.DataSource = DelvedW.Common.SearchResult(MyLocal.GetConnString(""), "tblBranch", makeqstr());
                grdSearchedBranch.DataBind();

                Branchentry.Visible = false;

                //ClearEntry();

                btnAddUpdDelBranch.Visible = true;
                BtnCancel.Visible = true;
                lblfrommassage.Text = "";

            }
        }
        private string CanItBeDeleted()
        {

            string mRetVal = "Yes";
            if (DelvedW.Common.IsExist(MyLocal.GetConnString(""), "tblBooking", "BCompID=" + lblBranchAID.Text + " Where (OurClientID=" + SVH.ClientID + ")"))
            {
                mRetVal = "No";
                lblfrommassage.Text = "Cannot Delete !! It is Already in Used in Booking";
            }



            return mRetVal;
        }

        protected void btnDelConfNo_Click(object sender, EventArgs e)
        {
            divdelconf.Visible = false;
            Branchentry.Visible = false;
            lblfrommassage.Text = " " + tbxBranchName.Text + " in NOT Deleted Successfully";
            grdSearchedBranch.DataSource = DelvedW.Common.SearchResult(MyLocal.GetConnString(""), "tblBranch", makeqstr());
            grdSearchedBranch.DataBind();
            //ClearEntry();

            btnAddUpdDelBranch.Visible = true;
            BtnCancel.Visible = true;
            // lblfrommassage.Text = "";

        }

        //protected void btnDOBCal_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (tbxSdob.Text.Trim() != "")
        //            cdrSDOB.SelectedDate = Convert.ToDateTime(tbxSdob.Text);
        //    }
        //    catch
        //    { }
        //    cdrSDOB.Visible = true;  //showing the calendar.

        //}

        //protected void cdrSDOB_SelectionChanged(object sender, EventArgs e)
        //{

        //    //tbxSdob.Text = String.Format("{0:MM/dd/yyyy}", cdrSDOB.SelectedDate);
        //    tbxSdob.Text = cdrSDOB.SelectedDate.ToShortDateString(); //Convert.ToDateTime(tbxSdob.Text);
        //    cdrSDOB.Visible = false; //hiding the calendar.
        //}

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            if (!(btnAddUpdDelBranch.Text.Contains("Save")))
            {
                searchBranch.Visible = true;
                Branchentry.Visible = false;
                ClearEntry();
                grdSearchedBranch.DataSource = null;
                grdSearchedBranch.DataBind();
                //grdSearchedBranch.Visible = false;
                divdelconf.Visible = false;
                lblfrommassage.Text = "";


            }
            ClearEntry();
            lblfrommassage.Text = "";

        }



        protected void grdSearchedBranch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "m")
            {

                lblBranchAID.Text = e.CommandArgument.ToString();

                GetBranchRecordValues(lblBranchAID.Text);

                btnAddUpdDelBranch.Enabled = true;
                Branchentry.Visible = true;
            }
        }

        protected void grdSearchedBranch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchDate();
            grdSearchedBranch.PageIndex = e.NewPageIndex;
            grdSearchedBranch.DataBind();
        }


    }

}