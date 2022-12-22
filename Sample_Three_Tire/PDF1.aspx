<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDF1.aspx.cs" Inherits="Sample_Three_Tire.PDF1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>
    <script type="text/javascript">
        function ConvertToImage(bntGeneratePDf) {
            html2canvas($("[id*=gvPdf]")[0]).then(function (canvas) {
                var base64 = canvas.toDataURL();
                $("[id*=hfImageData]").val(base64);
                __doPostBack(bntGeneratePDf.name, "");
            });
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div>
            <asp:GridView runat="server" ID="gvPdf" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="S.NO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="English Name">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("ENGLISH")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Telugu Name">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("DATA")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   

                </Columns>
            </asp:GridView>
        </div>
         <div>
              <asp:HiddenField ID="hfImageData" runat="server" />
            <asp:Button Text="GeneratePDF" ID="bntGeneratePDf" OnClick="bntGeneratePDf_Click"
                OnClientClick = "return ConvertToImage(this)" runat="server" UseSubmitBehavior="false" />
        </div>
    </form>
</body>
</html>
