<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="default.aspx.cs" Inherits="McSharesApp._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>McShares</title>
    <link rel="stylesheet" href="~/css/main.css"/>
</head>

<body>
    <form id="form1" runat="server">
        <div class="title">
            <h1>McShares</h1>                       
        </div>

        <div class="content" >
            <div class="label">
                <label>Please upload XML file:</label>
            </div>

            <asp:FileUpload ID="fileUpload" runat="server" />
            
            <br />        
        </div> 
        
                    <asp:Button ID="BtnValidate" runat="server" Text="Upload file" onclick="BtnValidate_Click"/>
                    <br />
                    <label>Validation Status:</label>
                    <asp:Label ID="LblStatus" runat="server" Text="No file Uploaded"></asp:Label>
                    <label></label>
 <h1></h1>
                    <div>
                        <label>Search Database by Name</label>
                        <asp:TextBox ID="SearchByname" runat="server"></asp:TextBox>
                        <asp:Button ID="SearchByTagButton" runat="server" Text="SEARCH" onclick="SearchByTagButton_Click" />
                    <table>
                <asp:DataList ID="customerSRCDL" runat="server" OnRowEditing="OnRowEditing" BorderColor="black" CellPadding="10" CellSpacing="10" RepeatDirection="Vertical" RepeatLayout="Table" BorderWidth="0">
                    
                        <HeaderTemplate>      
                              
                                <tr>      
                                    <th>Customer ID</th>                            
                                    <th>Customer Name</th>
                                    <th>Customer Type</th>
                                    <th>Date Birth/Incorporated</th>
                                    <th>Number of Shares</th>
                                    <th>Share Price per Unit</th>
                                    <th>Balance</th>     
                                </tr>      
                                 
                        </HeaderTemplate> 

                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center;"><%#Eval("CustomerID") %></td>
                                <td style="text-align:center;"><%#Eval("Name") %></td>
                                <td style="text-align:center;"><%#Eval("Type") %></td>
                                <td style="text-align:center;"><%# Eval("DOB").ToString() == "" ? Eval("DateIncorp") : Eval("DOB") %></td>
                                <td style="text-align:center;"><%#Eval("NumShares") %></td>
                                <td style="text-align:center;"><%#Eval("SharePrice") %></td>
                                <td style="text-align:center;"><%#Eval("Balance") %></td>

                               <td>
                               <asp:Button ID="updateBtn" runat="server" CommandName="update" Text="Edit" onclick="EditButton_Click"  /> 
                               </td>
                            </tr>
                        </ItemTemplate>  
                </asp:DataList>
            </table>
                    
                     <label></label>
                    
                  </div>

                   <h1></h1>
                   <div>
                    <asp:Button Text="Export CSV" OnClick="ExportCSV" runat="server" />
                   </div>
        
        
        <div class="customerTbl"> 
            <table>
                <asp:DataList ID="customerDL" runat="server" EnableViewState="true" OnItemCommand="dtlList_ItemCommand" CssClass="cus" BorderColor="black" CellPadding="10" CellSpacing="10" RepeatDirection="Vertical" RepeatLayout="Table" BorderWidth="0">
                        <HeaderTemplate>      
                                <tr>      
                                    <th>Customer ID</th>                            
                                    <th>Customer Name</th>
                                    <th>Customer Type</th>
                                    <th>Date Birth/Incorporated</th>
                                    <th>Number of Shares</th>
                                    <th>Share Price per Unit</th>
                                    <th>Balance</th>     
                                </tr>      
                                 
                        </HeaderTemplate> 
                        <ItemTemplate>
                            <tr>
                                <td style="text-align:center;"><%#Eval("CustomerID") %></td>
                                <td style="text-align:center;"><%#Eval("Name") %></td>
                                <td style="text-align:center;"><%#Eval("Type") %></td>
                                <td style="text-align:center;"><%# Eval("DOB").ToString() == "" ? Eval("DateIncorp") : Eval("DOB") %></td>
                                <td style="text-align:center;"><%#Eval("NumShares") %></td>
                                <td style="text-align:center;"><%#Eval("SharePrice") %></td>
                                <td style="text-align:center;"><%#Eval("Balance") %></td>

                               <td>
                               <asp:Button ID="Edit" runat="server" CommandName="Edit" Text="Edit"/>
                               </td>
                            </tr>
                        </ItemTemplate>  
                     <EditItemTemplate>
                        <ItemTemplate>
                            <tr>
                                <asp:TextBox id="Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:TextBox><br />
                                <asp:TextBox id="Type" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Type") %>'></asp:TextBox><br />
                                <asp:TextBox id="DOB" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DOB").ToString() == ""? Eval("DateIncorp") : Eval("DOB")%>'></asp:TextBox><br />
                                <asp:TextBox id="NumShares" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NumShares") %>'></asp:TextBox><br />
                                <asp:TextBox id="SharePrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SharePrice") %>'></asp:TextBox><br />
                                <td style="text-align:center;"><%=Balance%></td>

                                <asp:Button ID="Update" runat="server" CommandName="Update" Text="Update"/>
                            </tr>
                            </ItemTemplate>
                     </EditItemTemplate>  
                </asp:DataList>
            </table>
        </div>
        </form>
</body>
</html>
