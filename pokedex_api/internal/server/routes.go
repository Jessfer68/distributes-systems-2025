package server

import (
	"encoding/json"
	"errors"
	"net/http"

	apiErrors "github.com/Jessfer68/pokedexapi/internal/errors"
	"github.com/gorilla/mux"
)

func (s *Server) getPokemonByIdHandler(w http.ResponseWriter, r *http.Request) {
	id := mux.Vars(r)["id"]
	pokemon, err := s.pokemonService.GetPokemonById(id)
	if err != nil {
		if errors.Is(err, apiErrors.ErrPokemonNotFound) {
			writeJSON(w, http.StatusNotFound,
				map[string]string{"error": apiErrors.ErrPokemonNotFound.Error()})
			return
		}
		writeJSON(w, http.StatusInternalServerError,
			map[string]string{"error": apiErrors.ErrPokemonApi.Error()})
		return
	}
	writeJSON(w, http.StatusOK, pokemon)
}

func writeJSON(w http.ResponseWriter, statusCode int, data interface{}) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(statusCode)
	json.NewEncoder(w).Encode(data)
}
