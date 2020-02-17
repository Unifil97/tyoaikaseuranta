<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formi.aspx.cs" Inherits="tyoaikaseuranta.formi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Style.css" type="text/css" rel="stylesheet" />  
    <style type="text/css">
        /*#Select1 {
            height: 56px;
            width: 100px;
        }*/
    </style>
    </head>
<body>
   <form id="form1" runat="server">
    <div id="div1">
       
        
    <table>
        
        <asp:HiddenField ID="hftyot" runat="server" />
        <tr>
           <td>
               <asp:Label ID="lbpaikka" runat="server" Text="Paikka"></asp:Label>
             </td>
               <td colspan="2">
                   <asp:TextBox ID="txbpaikka" runat="server"></asp:TextBox>
               </td>
            </tr>
        <tr>
            <td>
               <asp:Label ID="lbtyoaika" runat="server" Text="Tyoaika"></asp:Label>
             </td>
               <td colspan="2">
                   <asp:TextBox ID="txbtyoaika" runat="server"></asp:TextBox>
               </td>
        </tr>
         <tr>
             <td>
               <asp:Label ID="lbpvm" runat="server" Text="Pvm"> </asp:Label>
     
             </td>
             <td> 
             <asp:Calendar ID="Calendar1" runat="server"
        onselectionchanged="Calendar1_SelectionChanged" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="2px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px" BorderStyle="Double">
            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
            <OtherMonthDayStyle BorderColor="#FF0066" ForeColor="#CC9966" />
            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
            <SelectorStyle BackColor="#FFCC66" />
            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
        </asp:Calendar>
                 </td> 
             </tr>
             <tr>
             <td colspan="2">
                   <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
               </td>
                
            </tr>
            <tr>
             <td>
               <asp:Label ID="lbylityo" runat="server" Text="Ylityot"></asp:Label>
             </td>
               <td colspan="2">
                   <asp:TextBox ID="txbylityo" runat="server"></asp:TextBox>
               </td>
            </tr>
            <tr>
             <td>
               
                 </td>
                 </tr>
                   <tr>
             <td draggable="true">
               <asp:Label ID="lbmatkat" runat="server" Text="Matkat"></asp:Label>
             </td>
               <td colspan="2">
                   <asp:TextBox ID="txbmatkat" runat="server" Text="0"></asp:TextBox>
               </td>
                <td>
                              &nbsp;</td>
            </tr>
        <tr>
               <td colspan="2">
                   <asp:Button ID="bttallenna" runat="server" Text="Tallenna" OnClick="bttallenna_Click" />
                   <asp:Button ID="btpoista" runat="server" Text="Poista" OnClick="btpoista_Click" OnClientClick = "Confirm()" />
                   <asp:Button ID="bttyhjenna" runat="server" Text="Tyhjennä" OnClick="bttyhjenna_Click" />

               </td>
                </tr>
        
            <tr>
            
               <td colspan="2">
                   <asp:Label ID="lbonnistui" runat="server" Text="" ForeColor="Green"></asp:Label>
               </td>
                
               <td>
                   <asp:Label ID="lbeionnistu" runat="server" Text="" ForeColor="Red"></asp:Label>
               </td>
          </tr>
          
    </table>
     
       
        <br />
        <asp:GridView ID="GridView1" runat="server" Width="681px" AutoGenerateColumns="False"   ShowFooter="True" PageSize="5" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"  >
              
        <Columns>
         <asp:BoundField DataField="Paikat" HeaderText="Työpaikka" />
         <asp:BoundField DataField="Pvm" HeaderText="Pvm" dataformatstring="{0:dd. MM . yyyy}"  />
         <asp:BoundField DataField="Tyoajat" HeaderText="Työajat" />
         <asp:BoundField DataField="Ylityo" HeaderText="Ylityö" />
             <asp:BoundField DataField="Matkat" HeaderText="Matkat" />
         <asp:TemplateField>
             <ItemTemplate>
             <asp:LinkButton runat="server" CommandArgument='<%# Eval("Id") %>' ForeColor="White" OnClick="lnk_OnClick" >Katso</asp:LinkButton>
           
             </ItemTemplate>
         
         </asp:TemplateField>
            
           
        </Columns>
    </asp:GridView>
       
   
       <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Kaikki matkat yhteensä"></asp:Label>
    </div>
    </form> 
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Haluatko varmasti poistaa?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</body>
</html>
