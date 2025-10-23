# Cassandra Naming Conventions

Apache Cassandra does not strictly enforce a single naming standard. Therefore, the following naming standard is based on the **most common and highly recommended convention** for **clusters, keyspaces, tables, columns, and indexes**.

<br />

> [!IMPORTANT]
> &nbsp;  
> 🐍 As a general rule, use **lower-case, snake\_case** for naming keyspaces, tables, and columns.  
> &nbsp;  

<br />

---

## 🤓☝️ Recommended Naming Convention

The primary recommendation is **`snake_case`** (lowercase with words separated by underscores):

- **Keyspace Names:** `my_application_keyspace`
- **Table Names:** `user_profiles`, `orders_by_customer`
- **Column Names:** `first_name`, `product_id`, `created_at`

### Case Sensitivity

<br />

> [!IMPORTANT]
> &nbsp;  
> Cassandra is case-insensitive for unquoted identifiers (they are automatically folded to lowercase during storage and comparison)  
> &nbsp;  

<br />

- By default, names are case-insensitive (MyTable = mytable).
- To enforce case sensitivity or use mixed case, enclose the name in double quotes ("MyTable" ≠ "mytable").​
- Best practice: use all lowercase identifiers to simplify query readability and schema management.

### Key Reasons for Lowercase/Snake\_Case

- **Case Insensitivity by Default:** By default, Cassandra Query Language (CQL) treats unquoted identifiers (keyspace, table, or column names) as **case-insensitive** and converts them to **lowercase** internally.
    * **Example:** If you define a table as `CREATE TABLE UserProfiles`, Cassandra stores it as `userprofiles` (all lowercase).

- **Avoid Quoting:** To use mixed-case names (e.g., `userProfile`), you **must** enclose the name in double quotes, like `"userProfile"`. This is generally discouraged as it:
    * Makes queries cumbersome.
    * Forces case-sensitivity, meaning you *must* use the exact case in every query (`SELECT * FROM "userProfile"`).

- **Client Driver Compatibility:** Many client drivers and object mappers often default to or strongly support mapping between language conventions and Cassandra's **`snake_case`** for columns.

---

## 📏 Naming Rules and Limits

| Component | Character Restrictions | Example | Case Handling (Unquoted) |
| :--- | :--- | :--- | :--- |
| **Keyspace Name** | Alphanumeric characters and **underscores** (`_`). Must start with an alphanumeric character. | `sales_app`, `analytics` | **Case-insensitive** (converted to lowercase). |
| **Table Name** | Alphanumeric characters and **underscores** (`_`). Must start with an alphanumeric character. | `users_by_country`, `orders_by_date` | **Case-insensitive** (converted to lowercase). |
| **Column Name** | Any character is technically supported, but using only alphanumeric characters and **underscores** (`_`) is best practice. | `user_id`, `created_at` | **Case-insensitive** (converted to lowercase). |
| **Cluster Name** | Any character is technically supported, but using only alphanumeric characters and **underscores** (`_`) is best practice. | `prod_ecommerce_cluster` | **Case-insensitive** (converted to lowercase). |
| **Index Name** | Any character is technically supported, but using only alphanumeric characters and **underscores** (`_`) is best practice. It follows the format `<table>_<indexed_column(s)>_<index_type>`. See [Index Naming Standards](#index-naming-standards) | `users_email_idx`, `user_zip_and_city_sai` | **Case-insensitive** (converted to lowercase). |

---

## 🏷️ Index Naming Standards

### Automatic Naming (Default)

If you omit the index name when using `CREATE INDEX`, Cassandra will automatically assign a name using this pattern:

```
<table_name>_<column_name>_idx
```

- **Example:**
  - For a table named `user_profiles` and a column `email`, the default index name would be **`user_profiles_email_idx`**.

### Recommended Manual Naming

While the default name is descriptive, it's often better practice to **manually name** your index to be more concise or to clearly indicate the purpose of the index, especially for more complex index types (like those on map keys or collection entries).

The recommended standard is to stick to **`lower_snake_case`** for manual naming:

- **Format:** `<table>_<indexed_column(s)>_<index_type>`

- **Example (Standard Secondary Index - 2i):**
    ```cql
    CREATE INDEX user_email_idx ON user_profiles (email);
    ```

- **Example (SAI - Storage-Attached Index):**
  
  Since SAI indexes can often cover multiple columns, you might name it to reflect the query:

  ```cql
  CREATE CUSTOM INDEX user_zip_and_city_sai ON user_profiles (zip, city) USING 'StorageAttachedIndex';
  ```

### Case Sensitivity Rule

Just like with keyspaces and tables, **indexes are case-insensitive** by default.

- You should avoid using mixed-case names (e.g., `EmailIndex`) because it forces you to enclose the name in double quotes (e.g., `DROP INDEX "EmailIndex"`) every time you reference it.

- **Best Practice:** Use only **lowercase letters and underscores** (`_`) for the index name.

| Operation | Best Practice (Unquoted) | Avoid (Quoted) |
| :--- | :--- | :--- |
| **Creation** | `CREATE INDEX user_email_idx ON ...` | `CREATE INDEX "UserEmailIndex" ON ...` |
| **Dropping** | `DROP INDEX user_email_idx` | `DROP INDEX "UserEmailIndex"` |

---

## 📢 Important Naming Notes

- **Quoted Names:** If you enclose a name in **double quotes** (e.g., `"MyKeyspace"`), Cassandra preserves the case and special characters. **Do not use this unless absolutely necessary.**

- **Length:** Keyspace and table names have a maximum recommended length (historically 48 characters, though newer versions may support more, keeping it concise is best). Column names should also be kept **short** where possible, as they are repeated for every cell in a partition, which can add significant overhead if they are verbose.

---
