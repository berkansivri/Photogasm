<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="Photogasm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>

        function openModal() {
            $('#loginMain').modal({ show: true });
        }
    </script>
    <br />
    <asp:UpdatePanel runat="server" ID="uptMain" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="exifCss" runat="server" id="exifCssID">
                <asp:ImageButton runat="server" CssClass="pull-right" ID="btnCloseExif" Height="25" Width="25" ImageUrl="/Images/close.png" OnClick="btnCloseExif_Click" />
                <table class="table table-condensed">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Taken Date"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lbltaken_Date"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Camera Model"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblcamera_Model"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Focal Length"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblfocal_Length"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="ISO Speed"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lbliso_Speed"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Aperture Value"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblaperture_value"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Shutter Speed"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblshutter_Speed"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="X Resolution"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblhorizontal_Resolution"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Y Resolution"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblverticalResolution"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Color Space"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblColorSpace"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Bits Per Pixel"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblbitsPerPixel"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="Image Size"></asp:Label></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblimageSize"></asp:Label></td>
                                </tr>
                            </table>
            </div>
            <div class="exifCssExit" runat="server" id="exifCssExitID"></div>
            <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemPlaceHolder">

                <ItemTemplate>
                    <div class="imgPanel">
                        <div class="closePopUpComment" data-close="<%# Eval("UID") %>" onclick="btnClosePopUpComment(this)">
                            <asp:ImageButton runat="server" CssClass="btnExifPopUp" ImageUrl="/Images/info.png" ToolTip="Exif Details" CommandName='<%# Eval("PID") %>' ID="btnExifPopUp" OnClick="btnExifPopUp_Click" />
                        </div>
                        <div class="imgPanel-baslik">
                            <asp:HyperLink runat="server" ForeColor="Black" NavigateUrl='<%# GetRouteUrl("User", new System.Web.Routing.RouteValueDictionary { { "userUrl", Eval("Url").ToString() } })%>'><h1><%# Eval("userFName") %>&nbsp;<%# Eval("userSName") %></h1></asp:HyperLink>
                            <img src='<%# Eval("userImg") %>' alt="Card image cap">
                        </div>
                        <asp:ImageButton ID="s1" ToolTip="Rate" Enabled="false" runat="server" CssClass="pull-right" Style="margin-right: 5%;" ImageUrl='<%# Eval("imgStar") %>' />
                        <asp:Label ID="lblDesc" Style="margin-left: 6%; font: normal 15px Lobster" runat="server" ToolTip="Description" Text='<%# Eval("Disc")%>'></asp:Label>
                        <asp:Image runat="server" ImageUrl='<%# Eval("imgUrl") %>' alt="Card image cap" />
                        <asp:Label runat="server" CssClass="label label-primary lblProjectName" Text='<%# Eval("projectname") %>'></asp:Label>

                        <div class="imgPanel-info">
                            <asp:ImageButton runat="server" CssClass="imgPanel-like" CommandName='<%# Eval("UID")%>' imgId='<%# Eval("PID") %>' ID="btnLike" ImageUrl='<%# Eval("Liked") %>' OnClick="btnLike_Click" /><asp:Label runat="server" CssClass="badge likeClass" Style="background-color: #e26161" ID="lblTLike" Text='<%# Eval("tLike") %>' AssociatedControlID="lblTLike"></asp:Label>
                            <img runat="server" aria-pressed="true" ID="btnComment" src="https://image.flaticon.com/icons/svg/134/134808.svg" /><asp:Label runat="server" CssClass="badge commentClass" Style="background-color: #e26161" ID="lblTComment" Text='<%# Eval("tComment") %>' AssociatedControlID="lblTComment"></asp:Label>
                        </div>
                    </div>
                    <div class="post commentPanel" runat="server" id="pnlComment">
                        <div class="post-footer">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtComment" CssClass="form-control" placeholder="Add a comment"></asp:TextBox>
                                <asp:Button runat="server" CssClass="btn btn-primary" imgId='<%# Eval("PID") %>' ID="btnSendComment" OnClick="btnSendComment_Click" Style="margin-left: 3px;" Text="Send"></asp:Button>
                            </div>

                            <ul class="comments-list" id="ulComment" runat="server">
                                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="uptComment">
                                    <ContentTemplate>
                                        <asp:ListView runat="server" ID="listComment" ClientIDMode="Static" ItemPlaceholderID="itemPlaceHolder" DataSource='<%# Eval("CommentList")%>'>
                                            <ItemTemplate>
                                                <li class="comment" runat="server">
                                                    <a class="pull-left" href='<%# GetRouteUrl("User", new System.Web.Routing.RouteValueDictionary { { "userUrl", Eval("UserUrl").ToString() } })%>'>
                                                        <img class="avatar" src='<%# Eval("UserImg") %>' alt="avatar">
                                                    </a>
                                                    <div class="comment-body">
                                                        <div class="comment-heading">
                                                            <asp:HyperLink runat="server" ForeColor="Black" NavigateUrl='<%# GetRouteUrl("User", new System.Web.Routing.RouteValueDictionary { { "userUrl", Eval("UserUrl").ToString() } })%>'><h4 class="user"><%# Eval("UserFName") %>&nbsp;<%# Eval("UserSName")%></h4></asp:HyperLink>
                                                            <h5 class="time">&nbsp;&nbsp;(<%# Eval("CDate") %>)</h5>
                                                        </div>
                                                        <p><%# Eval("CText") %></p>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ul>
                        </div>
                    </div>


                </ItemTemplate>
            </asp:ListView>

            <div style="margin-left: auto; margin-right: auto; width: 55px;"
                id="divPostsLoader">
            </div>
            <div id="endContent"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
