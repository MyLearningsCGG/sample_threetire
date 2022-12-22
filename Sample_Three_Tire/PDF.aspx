<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDF.aspx.cs" Inherits="Sample_Three_Tire.PDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdn.rawgit.com/niklasvh/html2canvas/master/dist/html2canvas.min.js"></script>
    <script type="text/javascript">
        function ConvertToImage(btnExport) {
            debugger;
            html2canvas($("[id*=gvPdf]")[0]).then(function (canvas) {
                var base64 = canvas.toDataURL();
                $("[id*=hfImageData]").val(base64);
                __doPostBack(btnExport.name, "");
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
                    <asp:TemplateField HeaderText="First Name">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("First_Name")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("LAST_NAME")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Number">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("MOBILENUMBER")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hindi Name">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("HLANG")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Telugu Name">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("TLANG")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Urdu Name">
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("ULANG")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
        <div>
            <asp:Button Text="GeneratePDF" ID="bntGeneratePDf" OnClick="bntGeneratePDf_Click" runat="server" />
        </div>
        <div>
            <asp:HiddenField ID="hfImageData" runat="server" />
            <asp:Button ID="btnExport" Text="Export to Image" runat="server" UseSubmitBehavior="false"
                OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />
        </div>
    </form>
</body>
</html>
