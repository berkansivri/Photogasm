<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="Photogasm.MyPage" %>

<asp:Content ID="MyPage" ContentPlaceHolderID="MainContent" runat="server">
    <link href="http://fontawesome.io/assets/font-awesome/css/font-awesome.css" rel="stylesheet" media="screen">
    <script>
        $(document).ready(function () {
            $("#dvProject").hide();
            $("#dvLikes").hide();
            var x = document.getElementsByClassName("backColor2");
            x[0].style.color = "Blue";
            x[1].style.color = "Orange";
            $("#<%=btnProject.ClientID%>").click(function () {
                $("#dvPhoto").hide();
                $("#dvLikes").hide();
                $("#dvProject").show();

                var x = document.getElementsByClassName("backColor");
                x[0].style.color = "Blue";
                x[1].style.color = "Orange";
                var y = document.getElementsByClassName("backColor2");
                y[0].style.color = "";
                y[1].style.color = "";
                var z = document.getElementsByClassName("backColor3");
                z[0].style.color = "";
                z[1].style.color = "";

            });
            $("#<%=btnPhoto.ClientID%>").click(function () {
                $("#dvProject").hide();
                $("#dvLikes").hide();
                $("#dvPhoto").show();
                var x = document.getElementsByClassName("backColor2");
                x[0].style.color = "Blue";
                x[1].style.color = "Orange";
                var y = document.getElementsByClassName("backColor");
                y[0].style.color = "";
                y[1].style.color = "";
                var z = document.getElementsByClassName("backColor3");
                z[0].style.color = "";
                z[1].style.color = "";
            });
            $("#<%=btnLikes.ClientID%>").click(function () {
                $("#dvProject").hide();
                $("#dvPhoto").hide();
                $("#dvLikes").show();
                var x = document.getElementsByClassName("backColor3");
                x[0].style.color = "Blue";
                x[1].style.color = "Orange";
                var y = document.getElementsByClassName("backColor");
                y[0].style.color = "";
                y[1].style.color = "";
                var z = document.getElementsByClassName("backColor2");
                z[0].style.color = "";
                z[1].style.color = "";
            });
        });

        function openModal() {
            $('#imgModal').modal({ show: true });
        }
        function openLogin() {
            $('#imgModal').modal('hide');
            $('#loginMain').modal({ show: true });
        }

        function closeModal() {
            $('imgModal').modal('hide');
            var x = document.getElementsByClassName("modal-backdrop");
            x[0].style.visibility = "hidden";
        }

    </script>
    <div class="container">
        <div class="row user-menu-container square">
            <div class="col-md-12 user-details">
                <div class="row coralbg white">
                    <div class="col-md-6 col-lg-6 col-sm-6 col-xs-6 no-pad">
                        <div class="user-pad">
                            <h3>Welcome, I'm
                                <asp:Label runat="server" ID="lblName"></asp:Label></h3>
                            <hr />
                            <h4 class="white"><i class="fa fa-check-circle-o"></i>
                                <asp:Label runat="server" ID="lblDesc"></asp:Label></h4>
                            <h4 class="white"><i class="fa fa-envelope-o"></i>
                                <asp:Label runat="server" ID="lblEmail"></asp:Label></h4>
                            <button type="button" id="btnUpdate" runat="server" class="btn btn-labeled btn-success" onclick="location.href='/User/Profile.aspx';">
                                <span class="btn-label"><i class="fa fa-pencil"></i></span>Update</button>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6 col-sm-6 col-xs-6 no-pad">
                        <div class="user-image">
                            <asp:ImageButton ID="imgUser" runat="server" ImageUrl="/Avatar/default_user2.jpg" ImageAlign="Middle" CssClass="thumbnail img-responsive" Style="max-height: 150px; margin-bottom: 0px;" />
                        </div>
                    </div>
                </div>
                <div class="row overview">
                    <div class="col-md-4 text-center btn btn-default backColor" aria-pressed="true" runat="server" id="btnProject">
                        <h3>PROJECTS</h3>
                        <h3 class="h44">
                            <asp:Label runat="server" CssClass="backColor" ID="lblProjects"></asp:Label></h3>
                    </div>

                    <div class="col-md-4 text-center btn btn-default backColor2" aria-pressed="true" runat="server" id="btnPhoto">
                        <h3>PHOTOS</h3>
                        <h3 class="h44">
                            <asp:Label runat="server" CssClass="backColor2" ID="lblPhotos"></asp:Label></h3>
                    </div>

                    <div class="col-md-4 text-center btn btn-default backColor3" aria-pressed="true" runat="server" id="btnLikes">
                        <h3>LIKES</h3>
                        <h3 class="h44">
                            <asp:Label runat="server" CssClass="backColor3" ID="lblLikes"></asp:Label></h3>
                    </div>
                </div>
            </div>

        </div>
        <div class="container-fluid user-pad">
            <div class="row" id="dvPhoto">
                <asp:Button runat="server" ID="btnBackAllPhoto" CssClass="btn btn-primary btn-block" Visible="false" Text="Back All Photos" PostBackUrl='<%# Eval("url","/{0}")%>' />
                <asp:Label runat="server" ID="lblProjectName" CssClass="label label-info text-center center-block" Font-Bold="false" Font-Names="'Lobster'" ForeColor="GrayText" BackColor="Wheat" Font-Size="XX-Large" Visible="false" Text="test"></asp:Label><hr style="margin-top: 3px; margin-bottom: 3px;" />
                
                <asp:ListView runat="server" ID="lstPhoto" EnableModelValidation="true">
                    <ItemTemplate>
                        <div class=" col-lg-4 col-md-4 col-sm-6 col-xs-12 gallery_product">
                            <asp:LinkButton runat="server" ID="btnImgLink">
                                <asp:ImageButton ID="imgPhoto" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"Path") %>' CssClass="thumbnail img-responsive projectlist" Style="max-height: 300px" imgId='<%# DataBinder.Eval(Container.DataItem,"PID")%>' runat="server" OnClick="imgPhoto_Click" ClientIDMode="Static" ata-toggle="modal" data-target="#imgModal" />
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>

                </asp:ListView>
            </div>
            <div class="row" id="dvProject">
                <asp:ListView runat="server" ID="lstProject" EnableModelValidation="true">
                    <ItemTemplate>
                        <div class=" col-lg-4 col-md-4 col-sm-6 col-xs-12 project_product">
                            <div class="projectProductImg">
                                <asp:ImageButton ID="imgProject" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"SamplePhoto")%>' CssClass="thumbnail img-responsive projectlist" ImageAlign="Middle" Style="max-width: 350px; vertical-align: middle;" projectId='<%# DataBinder.Eval(Container.DataItem,"ID")%>' projectName='<%# DataBinder.Eval (Container.DataItem,"Name")%>' runat="server" ClientIDMode="Static" OnClick="imgProject_Click" />
                            </div>
                            <asp:Label runat="server" ID="txtProjectName" CssClass="label center-block" ForeColor="Black" Font-Size="Medium" Text='<%# DataBinder.Eval (Container.DataItem,"Name")%>'>
                            </asp:Label>
                            <asp:Label runat="server" ID="txtPhotoCount" CssClass="center-block text-center"><span class="badge"><%#DataBinder.Eval(Container.DataItem,"PhotoCount")%></span> Photograph</asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="row" id="dvLikes">

                <asp:ListView runat="server" ID="lstLikes" EnableModelValidation="true">
                    <ItemTemplate>
                        <div class="project_product col-md-offset-3 col-lg-offset-3 col-lg-5 col-md-5 col-sm-5 col-xs-5">
                            <div class="col-md-offset-1 col-md-4 panell">
                                <asp:ImageButton ID="imgProject" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"imgUrl")%>' imgid='<%# DataBinder.Eval(Container.DataItem,"PID")%>' CssClass="thumbnail img-responsive projectlist" Width="200" Height="150" runat="server" ClientIDMode="Static" OnClick="imgPhoto_Click" />
                            </div>
                            <div class="col-md-offset-1 col-md-4 panell center-block">
                                <asp:HyperLink runat="server" NavigateUrl='<%# GetRouteUrl("User", new System.Web.Routing.RouteValueDictionary { { "userUrl", Eval("Url").ToString() } })%>' ForeColor="Black" CssClass="text-center">
                                    <asp:Image ID="imgLikesUser" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"userImg")%>' CssClass="img-circle img-thumbnail" Width="75" Height="75" runat="server" ClientIDMode="Static" /><br />
                                    <asp:Label runat="server" ID="txtProjectName" CssClass="label" ForeColor="Black" Font-Size="Medium" Text='<%# DataBinder.Eval(Container.DataItem, "userFName").ToString() + " " + DataBinder.Eval(Container.DataItem,"userSName").ToString()%>'></asp:Label>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="uptImgModal" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="exifCss" runat="server" id="exifCssID">
                    <asp:ImageButton runat="server" CssClass="pull-right" ID="btnCloseExif" OnClick="btnCloseExif_Click" Height="25" Width="25" ImageUrl="/Images/close.png" />
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
                

                <div tabindex="-1" class="modal fade-scale" id="imgModal" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content" runat="server">
                            <div class="modal-header">
                                    <asp:ImageButton runat="server" CssClass="btnExifPopUpp" ImageUrl="/Images/info.png" ToolTip="Exif Details" ID="btnExifPopUp" OnClick="btnExifPopUp_Click" />
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <div class="mdlUser">
                                    <div class="mdlUserImage">
                                        <asp:Image runat="server" CssClass="img-responsive img-circle center-block" ImageAlign="Middle" ID="userImg" />
                                    </div>
                                    <div class="mdlUserDetail">
                                        <asp:Label runat="server" CssClass=" " Font-Size="Larger" ID="phtNameSurname"></asp:Label>
                                        <asp:Label runat="server" CssClass=" " Font-Size="Larger" ID="phtEmail"></asp:Label>
                                    </div>

                                </div>
                            </div>

                            <div class="modal-body mdlBodyClass" id="imgModalBody">
                                <h4 class="modal-title ">
                                    <asp:Label runat="server" ID="phtProjectName" CssClass="label label-primary mdlProjectName"></asp:Label></h4>
                                <asp:Label ID="lblPhotoDesc" CssClass="mdlPhotoDesc" runat="server" ToolTip="Description"></asp:Label>
                                <div class="rateCss">
                                    <asp:ImageButton ID="s1" Enabled="false" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s2" Enabled="false" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s3" Enabled="false" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s4" Enabled="false" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s5" Enabled="false" runat="server" ImageUrl="/Images/ucstar.png" />
                                </div>
                                <asp:Image ID="imgW" CssClass="img-responsive" runat="server" ImageAlign="Middle" />
                            </div>
                            <div class="modal-footer">
                                <asp:UpdatePanel runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div class="mdlFooter">
                                            <asp:ImageButton runat="server" CssClass="imgPanel-like" ID="btnLike" OnClick="btnLike_Click" />
                                            <asp:Label CssClass="badge likeClasss" Style="background-color: #e26161;" ID="lblLikeCount" runat="server"></asp:Label>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnLike" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <button class="btn btn-primary" data-align="center" data-dismiss="modal" onclick="closeModal()">Close</button>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

