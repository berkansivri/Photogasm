<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Photogasm.WebForm1" %>
<asp:Content ID="ChangePassword" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Change Password</h1>
        <div class="row">
            <!-- left column -->
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="text-center">
                    <asp:Image ID="imgUser" runat="server" ImageUrl="Avatar/default_user.jpg" CssClass="avatar img-circle img-thumbnail" AlternateText="Profile Photo" />
                </div>
            </div>
            <!-- edit form column -->
            <div class="col-md-8 col-sm-6 col-xs-12 personal-info" runat="server">
                <div id="alertprofilepage" class="alert alert-info alert-dismissable" runat="server">
                    <a class="panel-close close" data-dismiss="alert">×</a>
                    <i class="glyphicon glyphicon-arrow-right"></i>
                    <%=alertchangepassword%>    
                </div>
                <h3>Personal info</h3>
                <div class="form-horizontal" role="form">
                    <div class="form-group hidden">
                        <label class="col-lg-3 control-label">ID:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtID" CssClass="form-control" runat="server" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">Email:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtProfileEmail" runat="server" TextMode="Email" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>

                    <hr />
                    <div>
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Current Password:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtCurrentPassword" CssClass="form-control" TextMode="Password" runat="server" ValidationGroup="profile2" MaxLength="20" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">New Password:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtNewPassword" CssClass="form-control" TextMode="Password" runat="server" ValidationGroup="profile1" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Confirm New Password:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtConfirmNewPassword" Text="" class="form-control" runat="server" value="" type="password" ValidationGroup="profile1" MaxLength="20"></asp:TextBox><asp:CompareValidator CssClass="registervalidator" ErrorMessage="Passwords do not match." ForeColor="Red" ControlToValidate="txtConfirmNewPassword" ControlToCompare="txtNewPassword" runat="server" ValidationGroup="profile1" />
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="col-md-6 control-label"></label>
                        <div class="col-md-6">
                            <asp:Button ID="btnSave" CausesValidation="true" CssClass="btn btn-primary" Text="Save Changes" runat="server" UseSubmitBehavior="true" OnClick="btnSave_Click" ValidationGroup="profile1" />

                            <span></span>
                            <asp:Button CausesValidation="false" ID="btnCancel" CssClass="btn btn-default" Text="Cancel" runat="server" UseSubmitBehavior="false" OnClick="btnCancel_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
