<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>Danh Sach Cong Ty</title>
      </head>
      <body>
        <h1>BẢNG LƯƠNG THÁNG</h1>
        <xsl:for-each select="DS">
          <xsl:for-each select="congty">
            <div border="1">
              <p>
                <b>
                  Tên công ty:
                </b>
                <xsl:value-of select="@TenCT"/>

              </p>
              <xsl:for-each select="donvi">
                <p>
                  <b>
                    Tên công ty:
                  </b>
                  <xsl:value-of select="tendv"/>
                </p>
                <table border="1" style="background-color: yellow; border-collapse:collapse" >
                  <tr>
                    <th>STT</th>
                    <th>Họ tên</th>
                    <th>Ngày sinh</th>
                    <th>Ngày công</th>
                    <th>Lương</th>
                  </tr>
                  <xsl:for-each select="nhanvien">
                    <tr>
                      <td>
                        <xsl:value-of select="position()"/>
                      </td>
                      <td>
                        <xsl:value-of select="hoten"/>
                      </td>
                      <td>
                        <xsl:value-of select="ngaysinh"/>
                      </td>
                      <td>
                        <xsl:value-of select="ngaycong"/>
                      </td>
                      <td>
                        <xsl:variable name="ngaycong" select="ngaycong"/>
                        <xsl:if test="$ngaycong &lt;= 20">
                          <xsl:value-of select="$ngaycong * 150000"/>
                        </xsl:if>
                        <xsl:if test="$ngaycong > 20 and $ngaycong &lt;= 25">
                          <xsl:value-of select="20 * 150000 + ($ngaycong - 20) * 200000"/>
                        </xsl:if>
                        <xsl:if test="$ngaycong > 25">
                          <xsl:value-of select="20 * 150000 + 5 * 200000 + ($ngaycong - 25) * 250000"/>
                        </xsl:if>
                      </td>
                    </tr>
                  </xsl:for-each>
                </table>
              </xsl:for-each>
            </div>
          </xsl:for-each>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
