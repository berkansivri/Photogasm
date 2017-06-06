<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyPage.aspx.cs" Inherits="Photogasm.MyPage3" %>

<asp:Content ID="MyPage3" ContentPlaceHolderID="MainContent" runat="server">
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
        function closeModal() {
            $('#imgModal').modal('hide');
        }

    </script>
    <div class="container">
        <div class="row user-menu-container square">
            <div class="col-md-12 user-details">
                <div class="row coralbg white">
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
                <asp:Button ID="btnDeleteProject" runat="server" Text="Delete Project" OnClick="DeleteProject_Click" CssClass="btn btn-danger" />
                <asp:Label runat="server" ID="lblProjectName" CssClass="label label-info text-center center-block" Font-Bold="false" Font-Names="'Lobster'" ForeColor="GrayText" BackColor="Wheat" Font-Size="XX-Large" Visible="false" Text="test"></asp:Label><hr style="margin-top: 3px; margin-bottom: 3px;" />
                <asp:ListView runat="server" ID="lstPhoto" EnableModelValidation="true">
                    <ItemTemplate>
                        <div class="gallery_product col-lg-4 col-md-4 col-sm-6 col-xs-12">
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
                        <div class="project_product col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <asp:ImageButton ID="imgProject" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"SamplePhoto")%>' CssClass="thumbnail img-responsive projectlist" ImageAlign="Middle" Style="max-height: 300px; vertical-align: middle;" projectId='<%# DataBinder.Eval(Container.DataItem,"ID")%>' projectName='<%# DataBinder.Eval (Container.DataItem,"Name")%>' runat="server" ClientIDMode="Static" OnClick="imgProject_Click" />
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
                                <asp:ImageButton ID="imgProject" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"imgUrl")%>' CssClass="thumbnail img-responsive projectlist" Width="200" Height="150" runat="server" ClientIDMode="Static" />
                            </div>
                            <div class="col-md-offset-1 col-md-4 panell">
                                <asp:HyperLink runat="server" NavigateUrl='<%# GetRouteUrl("User", new System.Web.Routing.RouteValueDictionary { { "userUrl", Eval("Url").ToString() } })%>' ForeColor="Black">
                                    <asp:Image ID="imgLikesUser" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"userImg")%>' CssClass="img-circle img-thumbnail" Width="75" Height="75" Style="margin-left: 57px; margin-top: 20px;" runat="server" ClientIDMode="Static" />
                                    <asp:Label runat="server" ID="txtProjectName" CssClass="label center-block" ForeColor="Black" Font-Size="Medium" Text='<%# DataBinder.Eval(Container.DataItem, "userFName").ToString() + " " + DataBinder.Eval(Container.DataItem,"userSName").ToString()%>'></asp:Label>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="uptImgModal" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label runat="server" ID="lblprojectID"></asp:Label>

                <div tabindex="-1" class="modal fade-scale" id="imgModal" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content" runat="server">
                            <div class="modal-header row">


                                <div class="col-md-3">
                                    <asp:Image runat="server" CssClass="img-responsive img-circle center-block" ImageAlign="Middle" ID="userImg" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" CssClass=" label label-default" Font-Size="Larger" ID="phtNameSurname"></asp:Label>
                                    <asp:Label runat="server" CssClass=" label label-default" Font-Size="Larger" ID="phtEmail"></asp:Label>
                                    <asp:Label runat="server" CssClass=" label label-default" Font-Size="Larger" ID="phtUrl"></asp:Label>
                                </div>


                            </div>

                            <div class="modal-body" id="imgModalBody">
                                <h4 class="modal-title">

                                    <asp:Label runat="server" ID="phtProjectName" CssClass="label label-primary col-md-3"></asp:Label></h4>
                                <div class="rateCss col-md-3 pull-right">
                                    <asp:ImageButton ID="s1" OnClick="imgPhotoEdit_Click" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s2" OnClick="imgPhotoEdit_Click" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s3" OnClick="imgPhotoEdit_Click" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s4" OnClick="imgPhotoEdit_Click" runat="server" ImageUrl="/Images/ucstar.png" />
                                    <asp:ImageButton ID="s5" OnClick="imgPhotoEdit_Click" runat="server" ImageUrl="/Images/ucstar.png" />
                                    Rate:
                                    <asp:Label ID="lblRate" runat="server"></asp:Label>
                                </div>
                                <asp:Label style="opacity:0;" runat="server" ID="deneme" Text="deneme" />
                                <asp:Label style="opacity:0;" runat="server" ID="deneme2" Text="deneme2" />
                                <asp:Image ID="imgW" CssClass="img-responsive" runat="server" ImageAlign="Middle" />
                            </div>
                            <asp:Label ID="lbl_comment" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txt_comment_edit" OnClick="imgPhotoEdit_Click" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="lbl_publish" runat="server" Text="Publish Photo"></asp:Label>
                            <asp:CheckBox ID="Chck_publish" OnClick="imgPhotoEdit_Click" runat="server" />

                            <asp:UpdatePanel runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="col-md-3 left">
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        
                           <button class="col-md-offset-8 col-md-2 btn btn-default bottom"  data-dismiss="modal">Close</button>
                        
                            
                           <div class="left">
                               <left>
                                
                            <asp:Button ID="editok" runat="server" Text="Edit Photo" CssClass="btn btn-success" OnClick="editok_Click" />
                            <asp:Button ID="Deletee" runat="server" Text="Delete Photo" OnClick="Deletee_Click" CssClass="btn btn-danger" />
                      
                            </left>
                             </div>
                            
                              
                      
                             

                            <%--      <asp:Button ID="addAnother2" runat="server" Text="Add Another Photo" CommandName='<%# Eval("Pro_ID") %>' OnClick="AddFoto" CssClass="btn btn-success" />   --%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

