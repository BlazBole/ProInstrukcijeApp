<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
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
				<!-- HEADER SECTION -->
				<div class="header">
					<div class="company-info">
						<!-- Podjetje, ki izdaja račun -->
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
						<!-- Podatki o računu -->
						<p>
							Številka: <strong>
								<xsl:value-of select="Racun/StevilkaRacuna" />
							</strong><br />
							Kraj: <strong>
								<xsl:value-of select="Racun/Kraj" />
							</strong><br />
							Datum: <strong>
								<xsl:value-of select="Racun/Datum" />
							</strong><br />
							Opravljeno: <strong>
								<xsl:value-of select="Racun/Opravljeno" />
							</strong><br />
							Zapade: <strong>
								<xsl:value-of select="Racun/Zapade" />
							</strong>
						</p>
					</div>
					<div class="clear"></div>
				</div>

				<!-- STRANKA -->
				<div class="customer-info">
					<p>
						<strong>
							<xsl:value-of select="Racun/Stranka/Ime" />
						</strong><br />
						<xsl:value-of select="Racun/Stranka/Naslov" /><br />
						Email: <xsl:value-of select="Racun/Stranka/Email" />
					</p>
				</div>

				<!-- POSTAVKE -->
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
						<xsl:for-each select="Racun/Postavke/Postavka">
							<tr>
								<td>
									<xsl:value-of select="Pozicija" />
								</td>
								<td>
									<xsl:value-of select="VrstaBlaga" />
								</td>
								<td>
									<xsl:value-of select="Kolicina" />
								</td>
								<td>
									<xsl:value-of select="EnotaMere" />
								</td>
								<td>
									<xsl:value-of select="CenaNaEnoto" />
								</td>
								<td>
									<xsl:value-of select="CenaPostavke" />
								</td>
							</tr>
						</xsl:for-each>
					</tbody>
				</table>

				<!-- MATERIAL -->
				<div>
					<p>
						<strong>Material</strong>
					</p>
					<ul class="material-list">
						<xsl:for-each select="Racun/Material/Postavka">
							<li>
								<xsl:value-of select="NazivMateriala" />
							</li>
						</xsl:for-each>
					</ul>
				</div>

				<!-- SKUPNA CENA -->
				<div class="total">
					<p>
						Delo: <strong>
							<xsl:value-of select="Racun/DeloSkupaj" />
						</strong>
					</p>
					<p>
						SKUPAJ ZA PLAČILO: <strong>
							<xsl:value-of select="Racun/SkupnaCena" />
						</strong> EUR
					</p>
				</div>

				<!-- FOOTER -->
				<div class="footer">
					<p>Račun izdala: STANTEHNIKA</p>
					<p>DDV ni obračunan v skladu s 94. členom ZDDV-1</p>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
