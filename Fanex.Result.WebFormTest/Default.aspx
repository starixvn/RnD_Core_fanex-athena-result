<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Fanex.Result.WebFormTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container">
        <fieldset>
            <legend>Project description </legend>
            <div>
                The library help you easily to display result of games.Currently Fanex.Result.Core
                supports to render HTML , excel and json string.and support 4 languages are english,
                chinese , traditional chinese and korean.
                <br />
                <br />
            </div>
            <div>
                <b>The structure of fanex.result.core:</b>
                <img src="DownLoad/untitled_page.png" alt="structure" /></div>
            <div>
                <ul>
                    <li>Release : version 1.0 - 07/01/2013 <a href="DownLoad/Fanex.Result.zip">1.0</a>
                    </li>
                    <li>Update : version 1.1 - 08/21/2013 - Support virtual horse racing <a href="DownLoad/Fanex.Result_rev1_1.zip">
                        1.1</a> </li>
                    <li>Update : version 1.2 - 10/01/2013 - Support virtual tennis <a href="DownLoad/Fanex.Result_rev1_2.zip">
                        1.2</a> </li>
                    <li>Update : version 1.3 - 11/18/2013 - Support ko-KR language. <a href="DownLoad/Fanex.Result_rev1_3.zip">
                        1.3</a> </li>
                    <li>Update : version 1.4 - 11/29/2013 <a href="DownLoad/Fanex.Result_rev1_4.zip">1.4</a>
                        <br />
                        Changes:
                        <ul>
                            <li>Suport virtual greyhounds and virutal motor racing. </li>
                            <li>Because Fanex.Result.Core.DTO.Racing is used for all racing sports now.We Renamed
                                HorseNo to EngineNo and HorseId to EngineId </li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div>
                <b>Source code:git@gito.nexcel.vn:licensee-risk-and-forecast/fanex-athena-result.git</b>
            </div>
        </fieldset>
        <fieldset>
            <legend>How to use? </legend>
            <div>
                <ul>
                    <li>Dowload:
                        <ul>
                            <li><a href="DownLoad/Fanex.Result_rev1_3.zip">Core & dll dependecy: </a></li>
                            <li><a href="DownLoad/Fanex.Result_CssTpl.zip">Css Template</a> </li>
                            <li><a href="DownLoad/Fanex.Result_ExcelTPl_rev1.2.zip">Excel Template</a> </li>
                        </ul>
                    </li>
                    <li>Installing:<br />
                        <b>Set up:</b>
                        <p>
                            * You only need to add reference Fanex.Result.Core to your project and put related
                            dll into bin folder.
                        </p>
                        <div>
                            * Required DLL:
                            <ul>
                                <li>Fanex.Result.Core </li>
                                <li>Fanex.Common.dll</li>
                                <li>RemotingRef.dll</li>
                                <li>IBCFile.dll (be used in remotingRef.dll)</li>
                            </ul>
                        </div>
                        <div>
                            * Put these files into your project if you want to:
                            <br />
                            <ul>
                                <li>Export Excel file:
                                    <ul>
                                        <li>NPOI.dll</li>
                                        <li>Put excel template in App_Data folder
                                            <br />
                                            <img src="DownLoad/templateGuide.png" alt="structure" />
                                        </li>
                                    </ul>
                                </li>
                                <li>html string:
                                    <ul>
                                        <li>System.Web.Razor.dll </li>
                                        <li>RazorEngine.dll</li>
                                        <li>pull css template into your project.
                                            <br />
                                            <img src="DownLoad/css_guide.png" alt="structure" />
                                        </li>
                                    </ul>
                                </li>
                                <li>Json String
                                    <ul>
                                        <li>Newtonsoft.Json </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li>Examples:
                        <div>
                            Render sportbook game:
                            <ul>
                                <li>
                                    <br />
                                    Transfer from your datatable to Fanex.Result.Core.DTO object.
                                    <br />
                                    <img src="DownLoad/dataaccess.png" alt="structure" style="width: 807px" />
                                </li>
                                <li>Initilize result setting:
                                    <br />
                                    <br />
                                    <img src="DownLoad/setting.png" alt="structure" />
                                </li>
                                <li>Export HTML:
                                    <pre>var htmlResult= ResultEngine.HtmlParser(data, setting);</pre>
                                </li>
                                <li>Export Excel:
                                    <pre>var _excelStream = ResultEngine.ExcelParser(data, setting);</pre>
                                </li>
                                <li>Export json:<pre> var jsonResult = ResultEngine.JsonParser(data,setting);</pre>
                                </li>
                            </ul>
                        </div>
                        <div>
                            <b>Get full example on LRF project:
                                <br />
                                GIT:git@gito.nexcel.vn:licensee-risk-and-forecast/main.git </b>
                            <br />
                            How to find result page code in LRF project:
                            <ul>
                                <li>1. Controller: LRF-Main\Fanex.Athena\Web\Fanex.Athena.Controllers\Report\ReportController.ResultPage.cs</li>
                                <li>2. Views:LRF-Main\Fanex.Athena\Web\Fanex.Athena\Views\Report\Result </li>
                                <li>3. Data service: Transfer data from datatable to Fanex.Result.Core.DTO objects.
                                    <br />
                                    LRF-Main\Fanex.Athena\Business\Fanex.Athena.Service\Implement\Report\ResultService.cs
                                </li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </fieldset>
    </div>
</asp:Content>
