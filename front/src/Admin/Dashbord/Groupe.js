import React, { useState, useEffect } from "react";
import {
  Box,
  Typography,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  IconButton,
  Snackbar,
  Divider,
  Pagination,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import GroupApi from "../../Api/GroupApi";

const Groupe = () => {
  const [groups, setGroups] = useState([]);
  const [openDialog, setOpenDialog] = useState(false);
  const [editingGroup, setEditingGroup] = useState(null);
  const [newGroup, setNewGroup] = useState({ nom: "" });
  const [successMessage, setSuccessMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [page, setPage] = useState(1);
  const groupsPerPage = 5;

  // Fetch all groups
  const fetchGroups = async () => {
    try {
      const data = await GroupApi.fetchGroups();
      setGroups(data);
    } catch (error) {
      setErrorMessage("Erreur lors de la récupération des groupes.");
      console.error("Erreur lors du fetch des groupes :", error);
    }
  };

  useEffect(() => {
    fetchGroups();
  }, []);

  // Handle form input changes
  const handleChange = (e) => {
    const { name, value } = e.target;
    if (editingGroup) {
      setEditingGroup({ ...editingGroup, [name]: value });
    } else {
      setNewGroup({ ...newGroup, [name]: value });
    }
  };

  // Add a new group
  const addGroup = async () => {
    if (!newGroup.nom.trim()) {
      setErrorMessage("Le nom du groupe est obligatoire !");
      return;
    }
    try {
      await GroupApi.createGroup(newGroup);
      setSuccessMessage("Groupe ajouté avec succès !");
      setOpenDialog(false);
      setNewGroup({ nom: "" });
      fetchGroups();
    } catch (error) {
      setErrorMessage("Erreur lors de l'ajout du groupe.");
      console.error("Erreur lors de la création :", error);
    }
  };

  // Update an existing group
  const updateGroup = async () => {
    if (!editingGroup.nom.trim()) {
      setErrorMessage("Le nom du groupe est obligatoire !");
      return;
    }
    try {
      await GroupApi.updateGroup(editingGroup.id, editingGroup);
      setSuccessMessage("Groupe modifié avec succès !");
      setOpenDialog(false);
      setEditingGroup(null);
      fetchGroups();
    } catch (error) {
      setErrorMessage("Erreur lors de la modification du groupe.");
      console.error("Erreur lors de la mise à jour :", error);
    }
  };

  // Delete a group
  const deleteGroup = async (id) => {
    if (!window.confirm("Êtes-vous sûr de vouloir supprimer ce groupe ?")) {
      return;
    }
    try {
      await GroupApi.deleteGroup(id);
      setSuccessMessage("Groupe supprimé avec succès !");
      fetchGroups();
    } catch (error) {
      setErrorMessage("Erreur lors de la suppression du groupe.");
      console.error("Erreur lors de la suppression :", error);
    }
  };

  // Open dialog for editing
  const openEditDialog = (group) => {
    setEditingGroup(group);
    setOpenDialog(true);
  };

  // Pagination logic
  const indexOfLastGroup = page * groupsPerPage;
  const indexOfFirstGroup = indexOfLastGroup - groupsPerPage;
  const currentGroups = groups.slice(indexOfFirstGroup, indexOfLastGroup);

  const handlePageChange = (event, newPage) => {
    setPage(newPage);
  };

  return (
    <Box sx={{ padding: 3 }}>
      <Typography variant="h4" fontWeight="bold" gutterBottom textAlign="center">
        Gestion des Groupes
      </Typography>
      <Divider sx={{ width: "100%", my: 2 }} />

      <Button
        variant="contained"
        color="success"
        onClick={() => {
          setEditingGroup(null);
          setNewGroup({ nom: "" });
          setOpenDialog(true);
        }}
      >
        + Ajouter un Groupe
      </Button>

      {/* Dialog Form */}
      <Dialog open={openDialog} onClose={() => setOpenDialog(false)} fullWidth>
        <DialogTitle textAlign="center">
          {editingGroup ? "Modifier un Groupe" : "Ajouter un Groupe"}
        </DialogTitle>
        <DialogContent>
          <TextField
            label="Nom du Groupe"
            name="nom"
            value={editingGroup ? editingGroup.nom : newGroup.nom}
            onChange={handleChange}
            variant="outlined"
            fullWidth
            required
            sx={{ mt: 2 }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenDialog(false)} color="inherit">
            Annuler
          </Button>
          <Button
            onClick={editingGroup ? updateGroup : addGroup}
            color="primary"
            variant="contained"
          >
            {editingGroup ? "Modifier" : "Ajouter"}
          </Button>
        </DialogActions>
      </Dialog>

      {/* Notifications */}
      <Snackbar
        open={!!successMessage || !!errorMessage}
        autoHideDuration={6000}
        onClose={() => {
          setSuccessMessage("");
          setErrorMessage("");
        }}
        message={successMessage || errorMessage}
        action={
          <Button
            color="inherit"
            size="small"
            onClick={() => {
              setSuccessMessage("");
              setErrorMessage("");
            }}
          >
            Fermer
          </Button>
        }
      />

      {/* Group Table */}
      <Table sx={{ mt: 3 }}>
        <TableHead>
          <TableRow>
            <TableCell style={{ fontWeight: "bold" }}>ID</TableCell>
            <TableCell style={{ fontWeight: "bold" }}>Nom</TableCell>
            <TableCell style={{ fontWeight: "bold" }}>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {currentGroups.map((group) => (
            <TableRow key={group.id}>
              <TableCell>{group.id}</TableCell>
              <TableCell>{group.nom}</TableCell>
              <TableCell>
                <IconButton color="primary" onClick={() => openEditDialog(group)}>
                  <EditIcon />
                </IconButton>
                <IconButton color="error" onClick={() => deleteGroup(group.id)}>
                  <DeleteIcon />
                </IconButton>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      {/* Pagination */}
      <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
        <Pagination
          count={Math.ceil(groups.length / groupsPerPage)}
          page={page}
          onChange={handlePageChange}
          color="success"
        />
      </Box>
    </Box>
  );
};

export default Groupe;
