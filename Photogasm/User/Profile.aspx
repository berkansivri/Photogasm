<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Photogasm.Profile" %>

<asp:Content ID="ProfilePage" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="page-header">Edit Profile</h1>
        <div class="row">
            <!-- left column -->
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="text-center">
                    <asp:Image ID="imgUser" runat="server" ImageUrl="Avatar/default_user.jpg" CssClass="img-circle img-thumbnail" AlternateText="Profile Photo" />
                    <h6 class="form-control">
                        <asp:Label ID="lblPhoto" runat="server" Text="Change Your Profile Photo (Max 3 Mb)"></asp:Label></h6>
                    <asp:FileUpload ID="imgUploadPath" runat="server" AllowMultiple="false" class="text-center center-block well well-sm" />
                    <asp:RegularExpressionValidator ID="vldPhotoUpload" runat="server" ErrorMessage="Only picture allowed! (.png|.bmp|.jpg|.jpeg)" Font-Size="Medium" Font-Bold="true" Font-Underline="true" ForeColor="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.png|.bmp|.jpg|.JPG|.jpeg|.JPEG)$" ControlToValidate="imgUploadPath" Display="Dynamic"></asp:RegularExpressionValidator>
                </div><br /><br />
                <asp:Button runat="server" CssClass="btn btn-block btn-info" ID="chgPass" OnClick="chgPass_Click" Text="Change Password" />
            </div>
            <!-- edit form column -->
            <div class="col-md-8 col-sm-6 col-xs-12 personal-info" runat="server">
                <div id="alertprofilepage" class="alert alert-info alert-dismissable" runat="server">
                    <a class="panel-close close" data-dismiss="alert">×</a>
                    <i class="glyphicon glyphicon-arrow-right"></i>
                    <%=alertprofile%>
                </div>
                <h3>Personal info</h3>
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="col-lg-3 control-label">ID:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtID" CssClass="form-control" runat="server" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group" role="form">
                        <label class="col-lg-3 control-label">First name:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtFName" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">Last name:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtSName" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Description:</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-lg-3 control-label">Email:</label>
                        <div class="col-lg-8">
                            <asp:TextBox ID="txtProfileEmail" runat="server" TextMode="Email" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-3 control-label">URL:</label>
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtWeb" runat="server" Text="teambro.azurewebsites.net/" Font-Size="Small" Enabled="false" CssClass="form-control" />
                        </div>
                        <div class="col-lg-4" style="padding-left: 0px; margin-left: 0px;">
                            <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" MaxLength="25" />
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
