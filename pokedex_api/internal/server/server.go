package server

import (
	"fmt"
	"log/slog"
	"net/http"
	"os"
	"strconv"

	"github.com/Jessfer68/pokedexapi/internal/service"
	"github.com/gorilla/mux"
)

type Server struct {
	pokemonService service.PokemonService
}

func NewServer() *http.Server {
	logger := slog.New(slog.NewTextHandler(os.Stdout, &slog.HandlerOptions{Level: slog.LevelInfo, AddSource: true}))
	slog.SetDefault(logger)

	port, _ := strconv.Atoi(os.Getenv("PORT"))
	//pokemonSoapUrl := os.Getenv("POKEMON_SOAP_URL")

	newServer := &Server{
		pokemonService: service.NewPokemonService(),
	}

	logger.Info("Start listening", "port", port)

	return &http.Server{
		Addr:    fmt.Sprintf(":%d", port),
		Handler: newServer.registerRoutes(),
	}
}

func (s *Server) registerRoutes() http.Handler {
	mux := mux.NewRouter()

	mux.HandleFunc("/pokemons/{id}", s.getPokemonByIdHandler).Methods(http.MethodGet)

	return s.corsMiddleware(mux)
}

func (s *Server) corsMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Access-Control-Allow-Origin", "*")
		next.ServeHTTP(w, r)
	})
}
