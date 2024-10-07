<?xml version="1.0" encoding="UTF-8"?>
<!--<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">-->
	<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns="http://www.w3.org/1999/xhtml"
				version="1.0"
				xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                                xmlns:ms="urn:schemas-microsoft-com:xslt"
                                exclude-result-prefixes="msxsl"
                                xmlns:user="urn:my-scripts"
				xmlns:cs="urn:cs"
>


		<xsl:output method="html" encoding="windows-1250" indent="yes"/>
	<xsl:template match="/">
		<html>
			<head>
				<style>
					body { font-family: Arial, sans-serif; }
					.container { width: 100%; }
					.header, .footer { width: 100%; margin-bottom: 20px; }
					.header .company-info { float: left; width: 50%; }
					.header .invoice-info { float: right; width: 50%; text-align: right; }
					.clear { clear: both; }
					table { width: 100%; border-collapse: collapse; margin-top: 20px; }
					table th, table td { border: 1px solid #000; padding: 8px; text-align: left; }
					table th { background-color: #f2f2f2; }
					.total { text-align: right; margin-top: 20px; font-weight: bold; }
					.footer { text-align: right; font-weight: bold; }
					.material-list { list-style-type: none; padding: 0; }
					.material-list li { margin-bottom: 5px; }
				</style>
			</head>
			<body>
				 HEADER SECTION 
				<div class="header">
					<div class="company-info">
						<p>
							<strong>STANTEHNIKA, Gregor Bole s.p.</strong><br />
							Borova ulica 9<br />
							2204 Miklavž na Dravskem polju<br />
							Davčna številka: 64979601<br />
							TRR: SI56 1010 0006 0376 869<br />
							Tel. št.: +386 51 677 041<br />
							Email: bole@stanttehnika.si
						</p>
					</div>
					<div class="invoice-info">
						 Podatki o računu 
						<p>
							Številka: <strong>
								<xsl:value-of select="RacunPodatki/Stevilka" />
							</strong><br />
							Kraj: <strong>
								<xsl:value-of select="RacunPodatki/Kraj" />
							</strong><br />
							Datum: <strong>
								<xsl:value-of select="RacunPodatki/Datum" />
							</strong><br />
							Opravljeno: <strong>
								<xsl:value-of select="RacunPodatki/DatumOpravljeno" />
							</strong><br />
							Zapade: <strong>
								<xsl:value-of select="RacunPodatki/DatumZapade" />
							</strong>
						</p>
					</div>
					<div class="clear"></div>
				</div>

				 STRANKA 
				<div class="customer-info">
					<p>
						<strong>
							<xsl:value-of select="Racun/FizicnaOseba/Ime" />
						</strong><br />
						<xsl:value-of select="Racun/FizicnaOseba/Naslov" /><br />
						Email: <xsl:value-of select="Racun/FizicnaOseba/ENaslov" />
					</p>
				</div>

				 POSTAVKE 
				<table>
					<thead>
						<tr>
							<th>Poz.</th>
							<th>Vrsta Blaga - storitev</th>
							<th>Kol.</th>
							<th>EM</th>
							<th>Cena</th>
							<th>EUR</th>
						</tr>
					</thead>
					<tbody>
						<xsl:for-each select="Racun/Postavke/Storitev">
							<tr>
								<td>
									<xsl:value-of select="EM" />
								</td>
								<td>
									<xsl:value-of select="Kolicina" />
								</td>
								<td>
									<xsl:value-of select="Cena" />
								</td>
								<td>
									<xsl:value-of select="CenaPostavke" />
								</td>
							</tr>
						</xsl:for-each>
					</tbody>
				</table>

				 FOOTER 
				<div class="footer">
					<p>Račun izdala: STANTEHNIKA</p>
					<p>DDV ni obračunan v skladu s 94. členom ZDDV-1</p>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
