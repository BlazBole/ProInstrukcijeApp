<?xml version="1.0" encoding="UTF-8"?>
<!--<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">-->
	<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns="http://www.w3.org/1999/xhtml"
				version="1.0"
				xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                                xmlns:ms="urn:schemas-microsoft-com:xslt"
                                exclude-result-prefixes="msxsl"
                                xmlns:user="urn:my-scripts"
				xmlns:cs="urn:cs">
		<xsl:output method="html" encoding="windows-1250" indent="yes"/>
	<xsl:template match="/">
		        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <style type="text/css">
                    /* Styling for A4 size page */
                    @page {
                        size: A4;
                        margin: 20mm;
                    }
					
                    body {
                        font-family: Arial, Helvetica;
                        font-size: 13pt;
                        margin: 0;
                        padding: 0;
                    }
					
                    table {
                        width: 100%;
                        border-collapse: collapse;
                    }
					
                    th, td {
						border: 1px solid gray;
                        padding: 2px;
                    }
					
                    .footer {
                        text-align: center;
                        margin-top: 20px;
                        font-size: 10pt;
                    }
					
					#invoiceInfoAtribut{
						font-weight: bolder;
					}
					
					#betwenColumn{
						width: 100px;
					}
					
					.tableNoGrid {
						border-collapse: collapse;
						width: 100%;
					}

					.tableNoGrid td, .tableNoGrid th {
						border: none;
					}

					.tableItems {
						border-collapse: collapse;
						width: 100%;
					}

					.tableItems td, .tableItems th {
						<!--border: 1px solid gray;-->
						padding: 8px;
						border: none;
					}
					
					.tableItems thead {
						display: table-row-group; /* Ne ponavljaj glave na več straneh */
					}
					
					.tableItems, .tableItems tr, .tableItems td {
						page-break-inside: avoid;
					}

					.HeaderItem {
						background-color: #f2f2f2;
					}

					.dataItem {
						text-align: center;
					}

					.dataItemName {
						text-align: left;
					}
				
					.tablePrice{
						border-collapse: collapse;
						width: 100%;
					}
					
				    .tablePrice th,  .tablePrice td{
						padding-right: 8px;
						border: none;
					}
					
					.alignRight{
						text-align: right;
					}
					
					.alignLeft{
						text-align: left;
					}
					
                </style>
            </head>
            <body>
                <!-- Glava -->
                <table class="tableNoGrid" style="margin-top: 10px">
                    <thead>
                        <tr>
                            <th align="left" style="padding-left: 70px">
								<img width="100px" height="100px" src="data:image/jpeg;base64, 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"></img>
						    </th>
							<td style="width: 20px;"></td>
                            <td align="left" valign="bottom" style="font-size: 14pt;">
								<strong>RAČUN</strong>
							</td>
						
                        </tr>
                    </thead>
					<tbody>
						<tr>
							<td colspan="3" style="height: 30px;"></td> <!-- Prazna vrstica z višino -->
						</tr>
						<tr>
							<td>STANTEHNIKA, Gregor Bole s.p.</td>
							<td id="betwenColumn"></td>
							<td></td>
						</tr>
						<tr>
							<td>Borova ulica 9,</td>
							<td id="betwenColumn"></td>
							<td></td>
						</tr>
					    <tr>
							<td>2204 Miklavž na Dravskem polju</td>
							<td id="betwenColumn"></td>
						    <td><strong>Številka: </strong> <xsl:value-of select="Racun/RacunPodatki/Stevilka" /> </td>
						</tr>
					    <tr>
							<td>Davčna številka: 64979601</td>
							<td id="betwenColumn"></td>
						    <td><strong>Kraj: </strong> <xsl:value-of select="Racun/RacunPodatki/Kraj" /> </td>
						</tr>
						<tr>
							<td>TRR: SI56 1010 0006 0376 869</td>
							<td id="betwenColumn"></td>
						    <td><strong>Datum: </strong> <xsl:value-of select="Racun/RacunPodatki/Datum" /> </td>
						</tr>
						<tr>
							<td>E-mail: bole@stantehnika.si</td>
							<td id="betwenColumn"></td>
						    <td><strong>Opravljeno: </strong> <xsl:value-of select="Racun/RacunPodatki/DatumOpravljeno" /> </td>
						</tr>
						<tr>
							<td>Tel. št.: +386 51 677 041</td>
							<td id="betwenColumn"></td>
						    <td><strong>Zapade: </strong> <xsl:value-of select="Racun/RacunPodatki/DatumZapade" /> </td>
						</tr>
						<!-- Podatki stranke -->
						<xsl:choose>
							<xsl:when test="Racun/FizicnaOseba">
								<tr>
									<td colspan="3" style="height: 30px;"></td> <!-- Prazna vrstica z višino -->
								</tr>
								<tr>
									<td colspan="3"><xsl:value-of select="Racun/FizicnaOseba/Ime" /></td>
								</tr>
								<tr>
									<td colspan="3"><xsl:value-of select="Racun/FizicnaOseba/Naslov" /></td>
								</tr>
								<tr>
									<td colspan="3"><xsl:value-of select="Racun/FizicnaOseba/ENaslov" /></td>
								</tr>
							</xsl:when>
						        <xsl:otherwise>
								<tr>
									<td colspan="3"><xsl:value-of select="Racun/Podjetje/Naziv" /></td>
								</tr>
								<tr>
									<td colspan="3"><xsl:value-of select="Racun/Podjetje/PE" /></td>
								</tr>
								<tr>
									<td colspan="3"><xsl:value-of select="Racun/Podjetje/DavcnaStevilka" /></td>
								</tr>
								<tr>
									<td colspan="3"><xsl:value-of select="Racun/Podjetje/ENaslov" /></td>
								</tr>
						</xsl:otherwise>
					</xsl:choose>
					</tbody>
                </table>

				<br></br>
				<br></br>
				
				<!-- Postavke -->
				<table class="tableItems">
					<thead>
						<tr>
							<th class="HeaderItem">Poz.</th>
							<th class="HeaderItem">Vrsta blaga - storitev</th>
							<th class="HeaderItem">Kol.</th>
							<th class="HeaderItem">EM</th>
							<th class="HeaderItem">Cena</th>
							<th class="HeaderItem">EUR</th>
						</tr>
					</thead>
					<tbody>
					<xsl:for-each select="Racun/Postavke/Postavka">
						<tr>
							<td class="dataItem"><xsl:value-of select="Poz"/></td>
							<td class="dataItemName"><xsl:value-of select="Storitev"/></td>
							<td class="dataItem"><xsl:value-of select="Kol"/></td>
							<td class="dataItem"><xsl:value-of select="Enota"/></td>
							<td class="dataItem"><xsl:value-of select="Cena"/></td>
							<td class="dataItem"><xsl:value-of select="Skupno"/></td>
						</tr>
					</xsl:for-each>
						<!-- Material -->
						<tr>
							<td></td>
							<td colspan="5">
								<ul>
								<xsl:for-each select="Racun/Material/MaterialItem">
									<li>
										<xsl:value-of select="."/>
									</li>
								</xsl:for-each>
								</ul>
							</td>
						</tr>
					</tbody>
				</table>
				
				<hr/>
				
				<table class="tablePrice">
					<thead>
						<tr>
							<th class="alignRight" colspan="2" style="font-size: 15xpt;"><strong>SKUPAJ ZA PLAČILO: <xsl:value-of select="Racun/RacunPodatki/SkupajZaPlacilo"/></strong></th>
						</tr>
					</thead>
					<tboady>
						<tr>
							<td class="alignRight" colspan="2">DDV ni obračunan v skladu s 94. členom ZDDV-1</td>
						</tr>
						<tr>
							<td colspan="2" style="height: 30px;"></td>
						</tr>
						<tr>
							<td class="alignLeft" colspan="2">
							<span style="font-size: 12pt;">Račun izdala: </span>
							<span style="font-size: 12pt; color:gray"><strong>STANTEHNIKA</strong></span>
							</td>
						</tr>
						<tr>
							<td colspan="2" style="height: 30px;"></td>
						</tr>
						<tr>
							<td>
							<span style="font-size: 12pt;">Podpis: </span>
							<span style="font-size: 12pt;">Gregor Bole</span>
							</td>
							<td>
							</td>
						</tr>
					</tboady>
				</table>
            </body>
        </html>
	</xsl:template>
</xsl:stylesheet>
