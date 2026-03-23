import type { ReactNode } from "react";

export interface ColumnDef<T> {
  header: string;
  accessor?: keyof T; 
  render?: (item: T) => ReactNode;
}

interface DataTableProps<T> {
  data: T[];
  columns: ColumnDef<T>[];
  keyExtractor: (item: T) => string | number;
  footer?: ReactNode;
}

export function DataTable<T>({ data, columns, keyExtractor, footer }: DataTableProps<T>) {
  return (
    <table style={{ width: "100%", borderCollapse: "collapse" }}>
      <thead>
        <tr style={{ background: "#f8fafc" }}>
          {columns.map((col, idx) => (
            <th
              key={idx}
              style={{
                padding: "10px 14px",
                textAlign: "left",
                fontSize: 12,
                fontWeight: 600,
                color: "#64748b",
                borderBottom: "1.5px solid #e2e8f0",
              }}
            >
              {col.header}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {data.map((item) => (
          <tr key={keyExtractor(item)} style={{ borderBottom: "1px solid #f1f5f9" }}>
            {columns.map((col, idx) => (
              <td key={idx} style={{ padding: "12px 14px" }}>
                {col.render
                  ? col.render(item)
                  : (item[col.accessor as keyof T] as ReactNode)}
              </td>
            ))}
          </tr>
        ))}
        {footer}
      </tbody>
    </table>
  );
}