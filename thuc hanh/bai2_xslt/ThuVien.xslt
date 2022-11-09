<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>Bai tap THU VIEN</title>
      </head>
      <body>
        <h1>DANH MỤC SÁCH</h1>
        <xsl:for-each select="ThuVien">
          <xsl:for-each select="NXB">
            <h3>
              Tên NXB <xsl:value-of select="@TenNXB"/>
            </h3>
            <table border="1" >
              <tr>
                <th>STT</th>
                <th>Tên Sách</th>
                <th>Số Trang</th>
                <th>Giá</th>
              </tr>
              <xsl:for-each select="Sach">
                <tr>
                  <td>
                    <xsl:value-of select="position()"/>
                  </td>
                  <td>
                    <xsl:value-of select="TenSach"/>
                  </td>
                  <td>
                    <xsl:value-of select="SoTrang"/>
                  </td>
                  <td>
                    <xsl:variable name="SoTrang" select="SoTrang"></xsl:variable>
                    <xsl:if test="$SoTrang &lt;= 100">
                      <xsl:value-of select="$SoTrang * 4000"/>
                    </xsl:if>
                    <xsl:if test="$SoTrang > 100 and $SoTrang &lt;= 150">
                      <xsl:value-of select="100 * 4000 + (150 - $SoTrang) * 3000"/>
                    </xsl:if>
                    <xsl:if test="$SoTrang > 150">
                      <xsl:value-of select="100 * 4000 + 50 * 3000 + ($SoTrang - 150) * 2000"/>
                    </xsl:if>
                  </td>
                </tr>
              </xsl:for-each>
            </table>
          </xsl:for-each>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
