import React, { useState } from 'react';
import { AppProvider } from './context/AppContext';
import { AppInitializer } from './AppInitializer';
import { Sidebar } from './components/organisms/Sidebar';
import { Dashboard } from './pages/Dashboard';
import { People } from './pages/Persons';
import { Categories } from './pages/Categories';
import { Transactions } from './pages/Transactions';

const AppContent: React.FC = () => {
  const [page, setPage] = useState("dashboard");

  return (
    <div style={{ display: "flex", minHeight: "100vh", fontFamily: "'DM Sans', 'Segoe UI', sans-serif", background: "#f1f5f9" }}>
      <Sidebar currentPage={page} setPage={setPage} />

      <main style={{ marginLeft: 200, flex: 1, padding: "36px 40px", maxWidth: "calc(100vw - 200px)" }}>
        {page === "dashboard"    && <Dashboard />}
        {page === "people"       && <People />}
        {page === "categories"   && <Categories />}
        {page === "transactions" && <Transactions />}
      </main>
    </div>
  );
};

export default function App() {
  return (
    <AppProvider>
      {/* FIX 2: carrega todos os dados na inicialização, antes de qualquer página */}
      <AppInitializer />
      <AppContent />
    </AppProvider>
  );
}
