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
               <asp:Label ID="lbtyoaika" runat="server" Text="Työaika"></asp:Label>
            </td>
               <td colspan="2">
                   <asp:TextBox ID="txbtyoaika" runat="server" Text="0:00"  ></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  controltovalidate="txbtyoaika" runat="server" ErrorMessage="Pakollinen kenttä Anna aika muodossa 0:00 " ValidationExpression="([0-9]|1[0-2]):[0-5][0-9]"></asp:RequiredFieldValidator>
                    <asp:regularexpressionvalidator id="regular1" controltovalidate="txbtyoaika" runat="server" errormessage="Anna aika muodossa 0:00" ValidationExpression="([0-9]|1[0-2]):[0-5][0-9]"></asp:regularexpressionvalidator>
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
                   <asp:TextBox ID="TextBox1" runat="server"  ></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="Textbox1" runat="server" ErrorMessage="Klikkaa päivämäärä"></asp:RequiredFieldValidator>
               </td>
                
            </tr>
            <tr>
             <td>
               <asp:Label ID="lbylityo" runat="server" Text="Ylityöt"></asp:Label>
             </td>
               <td colspan="2">
                   <asp:TextBox ID="txbylityo" runat="server" Text="0:00"></asp:TextBox>
                   <asp:regularexpressionvalidator id="regular" controltovalidate="txbylityo" runat="server" errormessage="Anna aika muodossa 0:00" ValidationExpression="([0-9]|1[0-2]):[0-5][0-9]"></asp:regularexpressionvalidator>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" controltovalidate="txbylityo" runat="server" ErrorMessage="Täytä kenttä Anna aika muodossa 0:00" ValidationExpression="([0-9]|1[0-2]):[0-5][0-9]"></asp:RequiredFieldValidator>
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
                   <asp:regularexpressionvalidator id="Regularexpressionvalidator1" controltovalidate="txbmatkat" runat="server" errormessage="Anna matka muodossa 0.00 tai yksittäinen numero! " ValidationExpression="^\d+(?:\.\d{1,2})?$" ></asp:regularexpressionvalidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" controltovalidate="txbmatkat" runat="server" ErrorMessage="Anna matka muodossa 0.00 tai yksittäinen numero!" ValidationExpression="^\d+(?:\.\d{1,2})?$"></asp:RequiredFieldValidator>
               </td>
                <td>
                </td>
            </tr>
        <tr>
               <td colspan="2">
                   <asp:Button ID="bttallenna" runat="server" Text="Tallenna" OnClick="bttallenna_Click"  />
                   <asp:Button ID="btpoista" runat="server" Text="Poista" OnClick="btpoista_Click" OnClientClick = "Confirm()" />
                   <asp:Button ID="bttyhjenna" runat="server" Text="Tyhjennä" OnClick="bttyhjenna_Click" value="refresh" />

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
        
       
         <div style="overflow-y: auto; overflow-x: hidden; height: 200px; width: 800px;">
        <asp:GridView ID="GridView1" runat="server" Width="800px" AutoGenerateColumns="False"    ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" ShowHeader="True" ShowHeaderWhenEmpty="true" >
             
        <Columns>
         <asp:BoundField DataField="Paikat" HeaderText="Työpaikka" />
         <asp:BoundField DataField="Pvm" HeaderText="Pvm" dataformatstring="{0:dd. MM . yyyy}"  />
         <asp:BoundField DataField="Tyoajat" HeaderText="Työajat" />
         <asp:BoundField DataField="Ylityo" HeaderText="Ylityö" />
             <asp:BoundField DataField="Matkat" HeaderText="Matkat" />
         <asp:TemplateField>
             <ItemTemplate>
             <asp:LinkButton runat="server" CommandArgument='<%# Eval("Id") %>' ForeColor="White" OnClick="lnk_OnClick" >Muuta</asp:LinkButton>
           
             </ItemTemplate>
         
         </asp:TemplateField>
            
           
        </Columns>
            <HeaderStyle BorderStyle="Groove" />
    </asp:GridView>
            </div>
        <br />
         <asp:TextBox ID="Textunnit" runat="server" ></asp:TextBox>
        
        
        <asp:Label ID="Label3" runat="server" Text="Työ tunnit yhteensä" ForeColor="White"></asp:Label>
        <br />
       <asp:TextBox ID="Texylityo" runat="server" ></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="Ylityöt yhteensä" ForeColor="White"></asp:Label>
        
       
        <br />
       <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
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
