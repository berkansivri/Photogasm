<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="Photogasm.CreateProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID%>").click();
            }
        }
        function closeExif() {
            var classNameExit = document.getElementsByClassName("exifCssExit");
            var className = document.getElementsByClassName("exifCss");
            classNameExit[0].style.transition = "0.8s";
            className[0].style.transition = "0.6s";

            classNameExit[0].style.transform = "scale(0)";
            className[0].style.transform = "translate(-50%, -50%) scale(0)";

            className[0].style.opacity = "0";
            classNameExit[0].style.opacity = "0";

        } function txtProjectName_Changed() {

            var x = document.getElementsByClassName("errorCss");
            var txtBox = document.getElementById("<%=lblProjectName.ClientID%>").value;
            var lblError = document.getElementById("<%=Errorlabel1.ClientID%>");

            switch (true) {
                case (txtBox.indexOf('ğ') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('Ğ') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('Ü') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('ü') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('Ö') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('ö') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('Ş') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('ş') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('Ç') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('ç') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('<') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('>') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('|') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('/') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('\\') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('*') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('.') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('ı') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('İ') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf('?') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                case (txtBox.indexOf(':') >= 0):
                    lblError.innerText = "Not allowed character.";
                    x[0].style.visibility = "visible";
                    break;
                default:
                    lblError.innerText = "";
                    x[0].style.visibility = "hidden";
                    break;
            }
        }
        function test() {
            var classNameExit = document.getElementsByClassName("exifCssExit");
            var className = document.getElementsByClassName("exifCss");

            classNameExit[0].style.transition = "0.8s";
            className[0].style.transition = "0.6s";

            classNameExit[0].style.transform = "scale(1)";
            className[0].style.transform = "translate(-50%, -50%) scale(1)";

            className[0].style.opacity = "1";
            classNameExit[0].style.opacity = "1";
            return false;
        }
        function openModal() {
            $('#loginMain').modal({ show: true });
        }

    </script>


    <asp:UpdatePanel ID="panel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-6">
                        <div class="exifCss" runat="server" id="exifCssID">
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
                        <div class="exifCssExit" runat="server" id="exifCssExitID" onclick="closeExif()"></div>
                  
                        <h1 class="sBaslikR">Create Project</h1>
                        <asp:Label ID="prj_write" runat="server" Text="" Visible="false"></asp:Label>
                        
                        <div class="input-group">
                         <span class="input-group-addon">Project Name</span>

                            <asp:TextBox ID="lblProjectName" CssClass="form-control" runat="server"> </asp:TextBox>
                            <div runat="server" id="errorCssID" class="errorCss">
                                <asp:Image runat="server" ImageUrl="https://image.flaticon.com/icons/svg/1/1268.svg" />
                                <asp:Label ID="Errorlabel1" runat="server"></asp:Label>
                            </div>
                        </div>
                            
                        <br />
                        <asp:Button ID="btnProject" CssClass="btn btn-success btn-block" runat="server" Text="Next" OnClick="CreatePrj" />

                        <asp:Label ID="filepath1" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:Label ID="IblStatus" runat="server" Text=""></asp:Label>
                        <div runat="server" class="uploadCss" id="uploadCssID">
                            <div class="fileUpload">
                                <div class="vUploadCss">
                                    <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Select Photo" />
                                </div>
                                <div class="UploadCss">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                            </div>
                       
                            <div class="hidden">
                                <asp:Button ID="btnUpload" runat="server" Text="Next" OnClick="btn_Upload" CssClass="btn btn-success btn-block" />
                            </div>
                        </div>
                        <div runat="server" id="sImgId" class="sImgCss thumbnail center">
                            <center>
                            <div class="projeImg">
                                <img src="Images/info.png" class="sImgCssInfo" onclick="test()"></img>
                                <img src="#" id="Image1" runat="server" />
                                <br />
                                <div class="rateCss">
                                    <asp:ImageButton ID="s1" runat="server" ImageUrl="~/Images/ucstar.png" OnClick="rateImage" />
                                    <asp:ImageButton ID="s2" runat="server" ImageUrl="~/Images/ucstar.png" OnClick="rateImage" />
                                    <asp:ImageButton ID="s3" runat="server" ImageUrl="~/Images/ucstar.png" OnClick="rateImage" />
                                    <asp:ImageButton ID="s4" runat="server" ImageUrl="~/Images/ucstar.png" OnClick="rateImage" />
                                    <asp:ImageButton ID="s5" runat="server" ImageUrl="~/Images/ucstar.png" OnClick="rateImage" />
                                    <asp:Label ID="lbl_rate" runat="server" Text=""></asp:Label>
                                </div>
                                <br />
                                <asp:CheckBox ID="chkpublish" runat="server" /><asp:Label ID="Label1" runat="server" Text="Publish Photo"></asp:Label>
                                 <br />
                                
                                <div class="caption">
                                    <div class="input-group">
                                        <span class="input-group-addon">Description</span>
                                        <asp:TextBox runat="server" ID="txtDesc" CssClass="form-control"></asp:TextBox>
                                    </div>
                                     <br />
                                </div>
                                
                            </div>

                            <br />
                             <br />
                           
                            <asp:Button ID="addAnother2" runat="server" Text="Add Another Photo" OnClick="AddFoto" CssClass="btn btn-success" />
                            <asp:Button ID="btnDone" runat="server" Text="Save All Project" OnClick="btnDone_Click" CssClass="btn btn-primary" />
                           </center>
                        </div>



                    </div>
                    <div class="col-md-3">
                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="lblProjectName" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
