<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Photogasm.Search" %>

<asp:Content ID="Search" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function searchhclick(e) {
            var key = (e.which) ? e.which : e.keyCode;
            if (key == 13) {
                __doPostBack('<%=btnSearchh.UniqueID%>', "");
            }
        }
    </script>

    <div class="h3 text-center" style="margin-top: 3%;">User Search</div>
    <asp:Panel runat="server">
        <div style="margin-left: 36%; margin-right: 36%; padding-top: 2%; margin-bottom: 1%;">
            <div class="input-group">
                <input class="form-control" placeholder="Search" autocomplete="off" runat="server" onkeypress="return searchhclick(event);" id="txtSearchh" />
                <div class="input-group-btn">
                    <button class="btn btn-default" id="btnSearchh" onserverclick="btnSearchh_ServerClick" runat="server" type="button"><i class="glyphicon glyphicon-search"></i></button>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-offset-3 col-sm-6 col-md-offset-3 col-md-6 col-lg-offset-3 col-lg-6">
                <div class="panel panel-default">
                    <ul class="list-group" id="contact-list">
                        <asp:ListView runat="server" ID="lstSearch">
                            <ItemTemplate>
                                <li class="list-group-item">
                                    <asp:HyperLink runat="server" NavigateUrl='<%# GetRouteUrl("User", new System.Web.Routing.RouteValueDictionary { { "userUrl", Eval("Url").ToString() } })%>' ForeColor="Black">
                                        <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                            <img src='<%# Eval("imgPath")%>' alt='<%#Eval("FName") + " " + Eval("SName")%>' class="img-responsive img-circle" />
                                        </div>
                                        <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9">
                                            <span class="name"><%#Eval("FName") + " " + Eval("SName")%></span><br /></asp:HyperLink>
                                    <span class="glyphicon glyphicon-envelope text-muted c-info" data-toggle="tooltip" title="E-mail"></span>
                                    <span class="text-muted"><%#Eval("Email")%></span><br />
                                    <span class="glyphicon glyphicon-pencil text-muted c-info" data-toggle="tooltip" title="Description"></span>
                                    <span class="text-muted"><%#Eval("Desc")%></span>
                                    </div>
                                        <div class="clearfix"></div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </ul>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
