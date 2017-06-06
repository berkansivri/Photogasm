using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fractions;

namespace Photogasm
{

    public partial class CreateProject : System.Web.UI.Page
    {
        public class ExifDet
        {
            public string _takendate { get; set; }
            public string _cameramodel { get; set; }
            public string _focal { get; set; }
            public string _isoSpeed { get; set; }
            public string _aperturevalue { get; set; }
            public string _shutterSpeed { get; set; }
            public string _verticalResolution { get; set; }
            public string _horizontalResolution { get; set; }
            public string _colorSpace { get; set; }
            public string _bitsPerPixel { get; set; }
            public string _imageSize { get; set; }
        }
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter ad;
        string imgUrl = "";
        List<ExifDet> Exif_Items;
        string _projectName;
        string takendate;
        string cameramodel;
        string focal;
        string isoSpeed;
        string aperturevalue;
        string shutterSpeed;
        string verticalResolution;
        string horizontalResolution;
        string colorSpace;
        string width;
        string height;
        string SizeOfImage;
        string bitsPerPixel;


        int i = 0;
        string _userID = "";
        string publish_checkbox = "0";


        private static Regex r = new Regex(":");

        protected void Page_Load(object sender, EventArgs e)
        {
            //CreateProject Session kontrol
            if (Session["loginuser"] != null)
            {
                _userID = Session["loginuser"].ToString();
                if (!IsPostBack)
                {
                    FileUpload1.Attributes["onchange"] = "UploadFile(this)";
                    lblProjectName.Attributes["OnKeyUp"] = "txtProjectName_Changed()";
                    btnDone.Attributes["OnClick"] = "goTop()";
                    addAnother2.Attributes["OnClick"] = "goTop()";
                    Image1.Src = "";
                }
            }
            else
                Response.Redirect("~/Default.aspx");

        }
        //fotoğraf ismini random atıyoruz
        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //proje create etmek için
        public void CreatePrj(object sender, EventArgs e)
        {
            var x = lblProjectName.Text;
            //sesli harf kontrolleri için
            switch (x.IndexOf("ğ", StringComparison.OrdinalIgnoreCase) >= 0
                || x.IndexOf("ü", StringComparison.OrdinalIgnoreCase) >= 0
                || x.IndexOf("ş", StringComparison.OrdinalIgnoreCase) >= 0
                || x.Contains("ı")
                || x.Contains(":")
                || x.Contains("İ")
                || x.Contains(".") || x.Contains(",")
                || x.IndexOf("ö", StringComparison.OrdinalIgnoreCase) >= 0
                || x.IndexOf("ç", StringComparison.OrdinalIgnoreCase) >= 0
                || x.Contains("*") || x.Contains("|") || x.Contains("/") || x.Contains("\\")
                || x.Contains("?") || x.Contains("<") || x.Contains(">"))
            {
                case true:
                    Errorlabel1.Text = "Not allowed character.";
                    errorCssID.Attributes.CssStyle.Add("visibility", "visible");
                    return;
            }

            string url = Server.MapPath("~/Images/" + _userID + "/");
            _projectName = lblProjectName.Text;
            //proje ismi var mı
            if (_projectName == "" || _projectName == null)
            {
                Errorlabel1.Text = "Please enter a project name.";
                errorCssID.Attributes.CssStyle.Add("visibility", "visible");
                return;
            }
            //proje ismi ilk başta space olamaz
            if (_projectName.StartsWith(" "))
            {
                Errorlabel1.Text = "Please don't start with space.";
                errorCssID.Attributes.CssStyle.Add("visibility", "visible");
                return;
            }
            try
            {
                if (!Directory.Exists(url + _projectName))
                {
                    errorCssID.Attributes.CssStyle.Add("visibility", "hidden");
                    Directory.CreateDirectory(url + _projectName);
                    Errorlabel1.Text = "";
                    uploadCssID.Attributes.CssStyle.Add("transform", "scale(1)");
                    lblProjectName.Enabled = false;
                    btnProject.Visible = false;
                    sqlProjeCreate(_projectName);
                }
                else
                {
                    Errorlabel1.Text = "Project name is exits.";
                    errorCssID.Attributes.CssStyle.Add("visibility", "visible");
                }
            }
            catch { }


        }
        public void btn_Upload(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
            {
                try
                {
                    //image türleri kontrolleri
                    if ((FileUpload1.PostedFile.ContentType == "image/jpeg") ||
                        (FileUpload1.PostedFile.ContentType == "image/jpg") ||
                        (FileUpload1.PostedFile.ContentType == "image/png") ||
                        (FileUpload1.PostedFile.ContentType == "image/bmp") ||
                        (FileUpload1.PostedFile.ContentType == "image/gif"))
                    {
                        if (FileUpload1.PostedFile.ContentLength < 3000000) //3MB MAX
                        {
                            _userID = Session["loginuser"].ToString();
                            _projectName = lblProjectName.Text;
                            string url = Server.MapPath("~/Images/" + _userID + "/");
                            string filename = Path.GetFileName(FileUpload1.FileName);
                            string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
                            string rndm = RandomString();
                            FileUpload1.SaveAs(url + _projectName + "/" + rndm + ext);
                            taken_Date(url + _projectName + "/" + rndm + ext);
                            camera_Model(url + _projectName + "/" + rndm + ext);
                            focal_Length(url + _projectName + "/" + rndm + ext);
                            iso_Speed(url + _projectName + "/" + rndm + ext);
                            aperture_value(url + _projectName + "/" + rndm + ext);
                            shutter_Speed(url + _projectName + "/" + rndm + ext);
                            vertical_Resolution(url + _projectName + "/" + rndm + ext);
                            horizontal_Resolution(url + _projectName + "/" + rndm + ext);


                            Image1.Src = "~/Images/" + _userID + "/" + _projectName + "/" + rndm + ext;

                            uploadCssID.Attributes.CssStyle.Add("transform", "scale(0)");
                            sImgId.Attributes.CssStyle.Add("transform", "scale(1)");
                            filepath1.Text = rndm + ext;
                            errorCssID.Attributes.CssStyle.Add("visibility", "hidden");

                        }
                        else
                        {
                            Errorlabel1.Text = "File must be max 3MB.";
                            errorCssID.Attributes.CssStyle.Add("visibility", "visible");
                        }
                    }
                    else
                    {
                        Errorlabel1.Text = "Only JPEG files are accepted.";
                        errorCssID.Attributes.CssStyle.Add("visibility", "visible");
                    }
                }
                catch (Exception exc)
                {
                    Errorlabel1.Text = "Something goes wrong please contact Administor.";
                    errorCssID.Attributes.CssStyle.Add("visibility", "visible");

                }
            }
        }
        protected void btnDone_Click(object sender, EventArgs e)
        {
            //Done butonu  için publish kontrol
            if (chkpublish.Checked)
            {
                publish_checkbox = "1";
            }
            else
            {
                publish_checkbox = "0";
            }

            Exif_Items = new List<ExifDet> { new ExifDet() {
                _shutterSpeed = lblshutter_Speed.Text,
                _aperturevalue = lblaperture_value.Text,
                _cameramodel = lblcamera_Model.Text,
                _focal = lblfocal_Length.Text,
                _isoSpeed = lbliso_Speed.Text,
                _takendate = lbltaken_Date.Text,
                _verticalResolution = lblverticalResolution.Text,
                _horizontalResolution = lblhorizontal_Resolution.Text,
                _colorSpace = lblColorSpace.Text,
                _bitsPerPixel=lblbitsPerPixel.Text,
                _imageSize=lblimageSize.Text
            } };


            sqlPhoto(Exif_Items);

            sImgId.Attributes.CssStyle.Add("transform", "translate(-50%, -50%) scale(0)");
            sImgId.Attributes.CssStyle.Add("transform", "scale(0)");
            lblProjectName.Enabled = true;
            btnProject.Visible = true;
            lblProjectName.Text = "";

            s1.ImageUrl = "./Images/ucstar.png";
            s2.ImageUrl = "./Images/ucstar.png";
            s3.ImageUrl = "./Images/ucstar.png";
            s4.ImageUrl = "./Images/ucstar.png";
            s5.ImageUrl = "./Images/ucstar.png";
            lbl_rate.Text = "";
            txtDesc.Text = "";
        }
        protected void AddFoto(object sender, EventArgs e)
        {
            Exif_Items = new List<ExifDet> { new ExifDet() {
                _shutterSpeed = lblshutter_Speed.Text,
                _aperturevalue = lblaperture_value.Text,
                _cameramodel = lblcamera_Model.Text,
                _focal = lblfocal_Length.Text,
                _isoSpeed = lbliso_Speed.Text,
                _takendate = lbltaken_Date.Text,
                _verticalResolution=lblverticalResolution.Text,
                _horizontalResolution=lblhorizontal_Resolution.Text,
                _colorSpace=lblColorSpace.Text,
                _bitsPerPixel=lblbitsPerPixel.Text,
                _imageSize=lblimageSize.Text
            } };

            //Add foto için publish kontrol
            if (chkpublish.Checked)
            {
                publish_checkbox = "1";
            }
            else
            {
                publish_checkbox = "0";
            }
            sqlPhoto(Exif_Items);

            sImgId.Attributes.CssStyle.Add("transform", "translate(-50%, -50%) scale(0)");
            sImgId.Attributes.CssStyle.Add("transform", "scale(0)");
            FileUpload1.Visible = true;
            uploadCssID.Attributes.CssStyle.Add("transform", "scale(1)");

            s1.ImageUrl = "./Images/ucstar.png";
            s2.ImageUrl = "./Images/ucstar.png";
            s3.ImageUrl = "./Images/ucstar.png";
            s4.ImageUrl = "./Images/ucstar.png";
            s5.ImageUrl = "./Images/ucstar.png";
            lbl_rate.Text = "";
            txtDesc.Text = "";
        }
        public void taken_Date(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))

                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    takendate = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    lbltaken_Date.Text = takendate;
                }

            }
            catch
            {
                takendate = "- No Value";
                lbltaken_Date.Text = "- No Value";
            }

        }
        public void camera_Model(string path)
        {

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(272);
                    cameramodel = Encoding.UTF8.GetString(propItem.Value);
                    lblcamera_Model.Text = cameramodel;
                }
            }
            catch
            {
                lblcamera_Model.Text = "- No Value";
                cameramodel = "- No Value";
            }
        }
        public void focal_Length(string path)
        {

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(37386);
                    //type 5
                    Fraction[] _resFraction = new Fraction[propItem.Len / (64 / 8)];
                    uint uNominator;
                    uint uDenominator;
                    for (int i = 0; i < _resFraction.Length; i++)
                    {
                        uNominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8));
                        uDenominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8) + (32 / 8));
                        _resFraction[i] = new Fraction(uNominator, uDenominator);
                    }
                    if (_resFraction.Length == 1)
                    {

                        focal = _resFraction[0].ToString() + "mm";
                        lblfocal_Length.Text = focal;
                    }

                    else
                    {
                        lblfocal_Length.Text = "- No Value";
                        focal = "- No Value";
                    }
                }
            }
            catch
            {
                lblfocal_Length.Text = "- No Value";
                focal = "- No Value";
            }
        }
        public void iso_Speed(string path)
        {

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(34855);
                    //type 5
                    ushort[] _resUshort = new ushort[propItem.Len / (16 / 8)];

                    for (int i = 0; i < _resUshort.Length; i++)
                    {
                        _resUshort[i] = BitConverter.ToUInt16(propItem.Value, i * (16 / 8));
                    }
                    if (_resUshort.Length == 1)
                    {
                        isoSpeed = _resUshort[0].ToString();
                        lbliso_Speed.Text = isoSpeed;
                    }


                    else
                    {
                        lbliso_Speed.Text = "- No Value";
                        isoSpeed = "- No Value";
                    }
                }
            }
            catch
            {
                lbliso_Speed.Text = "- No Value";
                isoSpeed = "- No Value";
            }
        }
        public void aperture_value(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(33437);
                    //type 5
                    Fraction[] _resFraction = new Fraction[propItem.Len / (64 / 8)];
                    uint uNominator;
                    uint uDenominator;
                    for (int i = 0; i < _resFraction.Length; i++)
                    {
                        uNominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8));
                        uDenominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8) + (32 / 8));
                        _resFraction[i] = new Fraction(uNominator, uDenominator);
                    }

                    if (_resFraction.Length == 1)
                    {
                        string value = _resFraction[0].ToString();
                        string x, y;
                        decimal s = 0;
                        switch (value.Contains("/"))
                        {
                            case true:
                                int start = value.IndexOf("/");
                                x = value.Substring(0, start);
                                y = value.Substring(start + 1, value.Length - start - 1);
                                s = Convert.ToDecimal(x) / Convert.ToDecimal(y);
                                break;

                            case false:
                                s = Convert.ToDecimal(value);
                                break;
                        }
                        aperturevalue = "f/" + s.ToString();
                        lblaperture_value.Text = aperturevalue;
                    }
                    else
                    {
                        lblaperture_value.Text = "- No Value";
                        aperturevalue = "- No Value";
                    }

                }
            }
            catch
            {
                lblaperture_value.Text = "- No Value";
                aperturevalue = "- No Value";
            }
        }
        public void shutter_Speed(string path)
        {

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {

                    PropertyItem propItem = myImage.GetPropertyItem(33434);
                    Fraction[] _resFraction = new Fraction[propItem.Len / (64 / 8)];
                    int sNominator;
                    int sDenominator;
                    for (int i = 0; i < _resFraction.Length; i++)
                    {
                        sNominator = BitConverter.ToInt32(propItem.Value, i * (64 / 8));
                        sDenominator = BitConverter.ToInt32(propItem.Value, i * (64 / 8) + (32 / 8));
                        _resFraction[i] = new Fraction(sNominator, sDenominator);
                    }
                    if (_resFraction.Length == 1)
                    {
                        shutterSpeed = _resFraction[0].ToString();

                        lblshutter_Speed.Text = shutterSpeed;
                    }

                    else
                    {
                        lblshutter_Speed.Text = "- No Value";
                        shutterSpeed = "- No Value";
                    }


                }
            }
            catch
            {
                lblshutter_Speed.Text = "- No Value";
                shutterSpeed = "- No Value";
            }

        }


        public void horizontal_Resolution(string path)
        {

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(282);
                    //type 5
                    Fraction[] _resFraction = new Fraction[propItem.Len / (64 / 8)];
                    uint uNominator;
                    uint uDenominator;
                    for (int i = 0; i < _resFraction.Length; i++)
                    {
                        uNominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8));
                        uDenominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8) + (32 / 8));
                        _resFraction[i] = new Fraction(uNominator, uDenominator);
                    }
                    if (_resFraction.Length == 1)
                    {

                        horizontalResolution = _resFraction[0].ToString() + " Dots Per Inch";
                        lblhorizontal_Resolution.Text = horizontalResolution;
                    }

                    else
                    {
                        lblhorizontal_Resolution.Text = "- No Value";
                        focal = "- No Value";
                    }
                }
            }
            catch
            {
                lblhorizontal_Resolution.Text = "- No Value";
                focal = "- No Value";
            }
        }
        public void vertical_Resolution(string path)
        {

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fs, false, false))
                {
                    width = myImage.Width.ToString();
                    height = myImage.Height.ToString();
                    SizeOfImage = width + "x" + height;

                    string pixelFormatFORcolorSpace = myImage.PixelFormat.ToString();
                    string pixelFormatFORbitsPerPixel = myImage.PixelFormat.ToString();

                    if (SizeOfImage.Length >= 2)
                    {

                        lblimageSize.Text = SizeOfImage;
                    }
                    else
                    {
                        SizeOfImage = "- No Value";
                        lblimageSize.Text = "- No Value";
                    }

                    if (pixelFormatFORbitsPerPixel.Length >= 1)
                    {
                        bitsPerPixel = pixelFormatFORbitsPerPixel.Remove(0, 6);
                        lblbitsPerPixel.Text = bitsPerPixel.ToString();
                    }
                    else
                    {
                        bitsPerPixel = "- No Value";
                        lblbitsPerPixel.Text = "- No Value";
                    }

                    if (pixelFormatFORcolorSpace.Length >= 1)
                    {
                        colorSpace = pixelFormatFORcolorSpace.Remove(0, 11);
                        lblColorSpace.Text = colorSpace.ToString();
                    }
                    else
                    {
                        colorSpace = "- No Value";
                        lblColorSpace.Text = "- No Value";
                    }

                    PropertyItem propItem = myImage.GetPropertyItem(283);
                    //width = myImage.Width.ToString();
                    //height = myImage.Height.ToString();
                    //SizeOfImage = width + "x" + height;

                    //type 5
                    Fraction[] _resFraction = new Fraction[propItem.Len / (64 / 8)];
                    uint uNominator;
                    uint uDenominator;
                    for (int i = 0; i < _resFraction.Length; i++)
                    {
                        uNominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8));
                        uDenominator = BitConverter.ToUInt32(propItem.Value, i * (64 / 8) + (32 / 8));
                        _resFraction[i] = new Fraction(uNominator, uDenominator);
                    }


                    if (_resFraction.Length == 1)
                    {
                        verticalResolution = _resFraction[0].ToString() + " Dots Per Inch";
                        lblverticalResolution.Text = verticalResolution;
                    }
                    else
                    {
                        lblverticalResolution.Text = "- No Value";
                        verticalResolution = "- No Value";
                    }

                }
            }
            catch
            {
                lblverticalResolution.Text = "- No Value";
                verticalResolution = "- No Value";

            }
        }


        protected void rateImage(object sender, ImageClickEventArgs e)
        {
            var senderImage = sender as ImageButton;
            var ID = senderImage.ID;
            switch (ID)
            {
                case "s1":
                    s1.ImageUrl = "./Images/cstar.png";
                    s2.ImageUrl = "./Images/ucstar.png";
                    s3.ImageUrl = "./Images/ucstar.png";
                    s4.ImageUrl = "./Images/ucstar.png";
                    s5.ImageUrl = "./Images/ucstar.png";
                    lbl_rate.Text = "Rate:1";
                    break;
                case "s2":
                    s1.ImageUrl = "./Images/cstar.png";
                    s2.ImageUrl = "./Images/cstar.png";
                    s3.ImageUrl = "./Images/ucstar.png";
                    s4.ImageUrl = "./Images/ucstar.png";
                    s5.ImageUrl = "./Images/ucstar.png";
                    lbl_rate.Text = "Rate:2";
                    break;
                case "s3":
                    s1.ImageUrl = "./Images/cstar.png";
                    s2.ImageUrl = "./Images/cstar.png";
                    s3.ImageUrl = "./Images/cstar.png";
                    s4.ImageUrl = "./Images/ucstar.png";
                    s5.ImageUrl = "./Images/ucstar.png";
                    lbl_rate.Text = "Rate:3";
                    break;
                case "s4":
                    s1.ImageUrl = "./Images/cstar.png";
                    s2.ImageUrl = "./Images/cstar.png";
                    s3.ImageUrl = "./Images/cstar.png";
                    s4.ImageUrl = "./Images/cstar.png";
                    s5.ImageUrl = "./Images/ucstar.png";
                    lbl_rate.Text = "Rate:4";
                    break;
                case "s5":
                    s1.ImageUrl = "./Images/cstar.png";
                    s2.ImageUrl = "./Images/cstar.png";
                    s3.ImageUrl = "./Images/cstar.png";
                    s4.ImageUrl = "./Images/cstar.png";
                    s5.ImageUrl = "./Images/cstar.png";
                    lbl_rate.Text = "Rate:5";
                    break;
            }

        }
        //sql create proje connetion
        public void sqlProjeCreate(string projectName)
        {
            SqlTask.conn = new SqlConnection(SqlTask.connString);
            try
            {   //proje id ye random .
                Random r1 = new Random();
                int num = r1.Next(0, 99999999);
                string number = string.Format("{0:D8}", num);//8 digit al
                Random r2 = new Random();
                int num2 = r2.Next(0, 26);//26 ingilizce karakter al
                char letter = (char)('A' + num2);
                string _PRID = letter + number;

                SqlTask.conn.Open();
                cmd = new SqlCommand("INSERT INTO Projects VALUES(@PID,@nm,@uID)", SqlTask.conn);
                cmd.Parameters.AddWithValue("@PID", _PRID);
                cmd.Parameters.AddWithValue("@nm", projectName);
                cmd.Parameters.AddWithValue("@uID", _userID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw; }
            //Response.Redirect(Request.Url.AbsoluteUri);
            finally
            {
                SqlTask.conn.Close();
            }
        }
        //sql connection photo control
        public void sqlPhoto(List<ExifDet> exifDet)
        {
            _projectName = lblProjectName.Text;
            int index = lbl_rate.Text.IndexOf(":");
            string rate = lbl_rate.Text.Remove(0, index + 1);
            string _ProID;
            string _PhotoID;
            SqlTask.conn = new SqlConnection(SqlTask.connString);

            string imgPath = ("teambro.azurewebsites.net/Images/" + _userID + "/" + _projectName + "/" + filepath1.Text);
            try
            {

                SqlTask.conn.Open();
                cmd = new SqlCommand("SELECT Pro_ID FROM Projects WHERE Name = @projeName AND User_ID = @uID ", SqlTask.conn);
                cmd.Parameters.AddWithValue("@projeName", _projectName);
                cmd.Parameters.AddWithValue("@uID", _userID);
                _ProID = cmd.ExecuteScalar().ToString();

                Random r1 = new Random();
                int num = r1.Next(0, 99999999);
                string number = string.Format("{0:D8}", num);
                Random r2 = new Random();
                int num2 = r2.Next(0, 26);
                char letter = (char)('A' + num2);
                _PhotoID = letter + number;
                SqlTask.conn.Close();

                SqlTask.conn.Open();
                cmd = new SqlCommand("INSERT INTO Photos VALUES(@photoID,@uid,@rate,@tlike,@ppath,@publish,@desc,@prid) ", SqlTask.conn);
                cmd.Parameters.AddWithValue("@photoID", _PhotoID);
                cmd.Parameters.AddWithValue("@uid", _userID);
                cmd.Parameters.AddWithValue("@rate", rate);
                cmd.Parameters.AddWithValue("@tlike", "0");
                cmd.Parameters.AddWithValue("@ppath", imgPath);
                cmd.Parameters.AddWithValue("@publish", publish_checkbox);
                cmd.Parameters.AddWithValue("@desc", txtDesc.Text);
                cmd.Parameters.AddWithValue("@prid", _ProID);
                cmd.ExecuteNonQuery();
                SqlTask.conn.Close();
            }
            catch (Exception ex) { throw; }
            try
            {
                SqlTask.conn.Open();
                cmd = new SqlCommand("INSERT INTO Details VALUES(@pid,@camera,@iso,@focal,@a_value,@s_value,@p_date,@h_resolution,@v_resolution,@color_space,@bits_per_pixel,@image_size) ", SqlTask.conn);
                cmd.Parameters.AddWithValue("@pid", _PhotoID);
                cmd.Parameters.AddWithValue("@camera", exifDet[0]._cameramodel);
                cmd.Parameters.AddWithValue("@iso", exifDet[0]._isoSpeed);
                cmd.Parameters.AddWithValue("@focal", exifDet[0]._focal);
                cmd.Parameters.AddWithValue("@a_value", exifDet[0]._aperturevalue);
                cmd.Parameters.AddWithValue("@s_value", exifDet[0]._shutterSpeed);
                cmd.Parameters.AddWithValue("@p_date", exifDet[0]._takendate);
                cmd.Parameters.AddWithValue("@h_resolution", exifDet[0]._horizontalResolution);
                cmd.Parameters.AddWithValue("@v_resolution", exifDet[0]._verticalResolution);
                cmd.Parameters.AddWithValue("@color_space", exifDet[0]._colorSpace);
                cmd.Parameters.AddWithValue("@bits_per_pixel", exifDet[0]._bitsPerPixel);
                cmd.Parameters.AddWithValue("@image_size", exifDet[0]._imageSize);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw; }
            SqlTask.conn.Close();
        }


        protected void UploadImj(object sender, EventArgs e)
        {
            uploadCssID.Attributes.CssStyle.Add("transform", "scale(0)");
            sImgId.Attributes.CssStyle.Add("transform", "scale(1)");
        }
    }
}