import os
from pathlib import Path

import pandas as pd
from sqlalchemy import create_engine, text
from dotenv import load_dotenv

load_dotenv()

SERVER = os.getenv("DB_SERVER", "(localdb)\\MSSQLLocalDB")
DATABASE = os.getenv("DB_NAME", "VagtPlanDbV2")
USERNAME = os.getenv("DB_USERNAME", "")
PASSWORD = os.getenv("DB_PASSWORD", "")

if USERNAME and PASSWORD:
    CONNECTION_STRING = (
        f"mssql+pyodbc://{USERNAME}:{PASSWORD}@{SERVER}/{DATABASE}"
        "?driver=ODBC+Driver+17+for+SQL+Server"
    )
else:
    CONNECTION_STRING = (
        f"mssql+pyodbc://@{SERVER}/{DATABASE}"
        "?driver=ODBC+Driver+17+for+SQL+Server"
        "&Trusted_Connection=yes"
    )

engine = create_engine(
    CONNECTION_STRING, fast_executemany=True  # fast_executemany for big inserts
)

QUERY = text(
    """
    SELECT 
        u.Id AS BrugerId,
        u.FullName AS Navn,
        dt.Name AS Personalegruppe
    FROM AspNetUsers u
    LEFT JOIN DoctorTypes dt ON u.DoctorTypeId = dt.Id
    WHERE u.UserStatus = 0 -- Active users only
    ORDER BY u.FullName
"""
)


def export():
    """Export active personnel to CSV and JSON files."""
    df = pd.read_sql(QUERY, engine)

    output_path = Path(__file__).parent.parent
    csv_file = output_path / "active_personnel.csv"
    json_file = output_path / "active_personnel.json"

    df.to_csv(
        csv_file, index=False, encoding="utf-8", sep=";"
    )  # index=False to avoid writing row numbers
    df.to_json(
        json_file, orient="records", force_ascii=False, indent=2
    )  # orient="records" for array of objects,

    print(f"Exported {len(df)} active personnel to {csv_file} and {json_file}")


if __name__ == "__main__":
    export()
